﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiffMatchPatch;
using OMetaSharp;

using SharpDiff.Parsers.GitDiff;
using Diff = SharpDiff.Parsers.GitDiff.Diff;

namespace SharpDiff
{
  public static class Differ
  {

    #region parsing "diff --git" files

    public static IEnumerable<Diff> LoadGitDiff(string diffContent)
    {
      return Grammars.ParseWith<GitDiffParser>(diffContent, x => x.Diffs).ToIEnumerable<Diff>();
    }

    public static IEnumerable<Diff> LoadGitDiffSplit(string diffContent)
    {
      return SplitGitDiffs(diffContent).Select(ParseSingleGitDiff);
    }

    public static ParallelQuery<Diff> LoadGitDiffParallel(string diffContent)
    {
      return SplitGitDiffs(diffContent).AsParallel().Select(ParseSingleGitDiff);
    }

    #region helpers

    internal static IEnumerable<string> SplitGitDiffs(string diffContent)
    {
      string regex = @"(?<=\r\n|\n)(?=diff --git)";
      return System.Text.RegularExpressions.Regex.Split(diffContent, regex);
    }

    internal static Diff ParseSingleGitDiff(string diffString)
    {
      return Grammars.ParseWith<GitDiffParser>(diffString, x => x.Diff).As<Diff>();
    }

    #endregion

    #endregion

    #region comparing files

    public static Diff Compare(string fileOnePath, string fileOneContent, string fileTwoPath, string fileTwoContent)
    {
      return Compare(fileOnePath, fileOneContent, fileTwoPath, fileTwoContent, new CompareOptions());
    }

    public static Diff Compare(string fileOnePath, string fileOneContent, string fileTwoPath, string fileTwoContent, CompareOptions options)
    {
      if (fileOneContent == null && fileTwoContent == null)
        throw new InvalidOperationException("Both files were null");

      if (options.BomMode == BomMode.Ignore) {
        if (fileOneContent != null)
          fileOneContent = RemoveBom(fileOneContent);
        if (fileTwoContent != null)
          fileTwoContent = RemoveBom(fileTwoContent);
      }

      if (fileTwoContent == null)
        return DeletedFileDiff(fileOneContent, fileOnePath);
      if (fileOneContent == null)
        return NewFileDiff(fileTwoContent, fileTwoPath);
      if (IsBinary(fileOneContent))
        throw new BinaryFileException(fileOnePath);
      if (IsBinary(fileTwoContent))
        throw new BinaryFileException(fileTwoPath);

      var patchMaker = new PatchMaker();
      var patches = patchMaker.MakePatch(fileOneContent, fileTwoContent, options);
      var chunks = new List<Chunk>();

      foreach (var patch in patches) {
        var originalRange = new ChangeRange(patch.Start1 + 1, patch.Length1);
        var newRange = new ChangeRange(patch.Start2 + 1, patch.Length2);
        var range = new ChunkRange(originalRange, newRange);
        var snippets = new List<ISnippet>();

        var lines = new List<DiffMatchPatch.Diff>();
        Operation? previousOperation = null;
        var isModification = false;

        foreach (var diff in patch.Diffs) {
          if (previousOperation == null)
            previousOperation = diff.Type;
          if (previousOperation == Operation.DELETE && diff.Type == Operation.INSERT)
            isModification = true;
          else if (previousOperation != diff.Type) {
            // different operation
            if (previousOperation == Operation.EQUAL)
              snippets.Add(new ContextSnippet(lines.Select(x => new ContextLine(x.Text)).Cast<ILine>()));
            else if (isModification)
              snippets.Add(new ModificationSnippet(
                  lines
                      .Where(x => x.Type == Operation.DELETE)
                      .Select(x => new SubtractionLine(x.Text))
                      .Cast<ILine>(),
                  lines
                      .Where(x => x.Type == Operation.INSERT)
                      .Select(x => new AdditionLine(x.Text))
                      .Cast<ILine>()
              ));
            else if (previousOperation == Operation.INSERT)
              snippets.Add(new AdditionSnippet(lines.Select(x => new AdditionLine(x.Text)).Cast<ILine>()));
            else if (previousOperation == Operation.DELETE)
              snippets.Add(new SubtractionSnippet(lines.Select(x => new SubtractionLine(x.Text)).Cast<ILine>()));

            lines.Clear();
            isModification = false;
          }

          lines.Add(diff);
          previousOperation = diff.Type;
        }

        if (lines.Count > 0) {
          if (isModification) {
            snippets.Add(new ModificationSnippet(
                lines
                    .Where(x => x.Type == Operation.DELETE)
                    .Select(x => new SubtractionLine(x.Text))
                    .Cast<ILine>(),
                lines
                    .Where(x => x.Type == Operation.INSERT)
                    .Select(x => new AdditionLine(x.Text))
                    .Cast<ILine>()
            ));
          } else if (previousOperation == Operation.INSERT)
            snippets.Add(new AdditionSnippet(lines.Select(x => new AdditionLine(x.Text)).Cast<ILine>()));
          else if (previousOperation == Operation.DELETE)
            snippets.Add(new SubtractionSnippet(lines.Select(x => new SubtractionLine(x.Text)).Cast<ILine>()));
          else
            snippets.Add(new ContextSnippet(lines.Select(x => new ContextLine(x.Text)).Cast<ILine>()));
        }

        chunks.Add(new Chunk(range, snippets));
      }

      var fileOne = new File('a', fileOnePath);
      var fileTwo = new File('b', fileTwoPath);
      var header = new DiffHeader(new DiffFormatType("generated"), new[] { fileOne, fileTwo });

      return new Diff(header, Array.Empty<IHeader>(), new ChunksHeader(fileOne, fileTwo), chunks);
    }

    #region helpers

    internal static bool IsBinary(string content)
    {
      // todo: make this more robust
      return content.Contains("\0\0\0");
    }

    internal static Diff DeletedFileDiff(string content, string path)
    {
      var fileOne = new File('a', path);
      var fileTwo = new NullFile();
      var header = new DiffHeader(new DiffFormatType("generated"), new IFile[] { fileOne, fileTwo });

      var lines = content.SplitIntoLines()
          .Select(x => (ILine)new SubtractionLine(x));
      var range = new ChunkRange(new ChangeRange(1, lines.Count()), new ChangeRange(0, 0));
      var snippet = new SubtractionSnippet(lines);
      var chunk = new Chunk(range, new[] { snippet });

      return new Diff(header, Array.Empty<IHeader>(), new ChunksHeader(fileOne, fileTwo), new[] { chunk });
    }

    internal static Diff NewFileDiff(string content, string path)
    {
      var fileOne = new NullFile();
      var fileTwo = new File('b', path);
      var header = new DiffHeader(new DiffFormatType("generated"), new IFile[] { fileOne, fileTwo });

      var lines = content.SplitIntoLines()
          .Select(x => (ILine)new AdditionLine(x));
      var range = new ChunkRange(new ChangeRange(0, 0), new ChangeRange(1, lines.Count()));
      var snippet = new AdditionSnippet(lines);
      var chunk = new Chunk(range, new[] { snippet });

      return new Diff(header, Array.Empty<IHeader>(), new ChunksHeader(fileOne, fileTwo), new[] { chunk });
    }

    internal static readonly List<byte[]> ByteOrderMarks = new List<byte[]>
    {
            new byte[] { 0x00, 0x00, 0xFE, 0xFF },
            new byte[] { 0xFF, 0xFE, 0x00, 0x00 },
            new byte[] { 0xEF, 0xBB, 0xBF },
            new byte[] { 0xFE, 0xFF },
            new byte[] { 0xFF, 0xFE },
        };

    internal static string RemoveBom(string content)
    {
      var encoding = new UTF8Encoding();
      var bytes = encoding.GetBytes(content);

      if (bytes.Take(4).ContainsOnly(ByteOrderMarks[0]) || bytes.Take(4).ContainsOnly(ByteOrderMarks[1]))
        return encoding.GetString(bytes.Skip(4).ToArray());

      if (bytes.Take(3).ContainsOnly(ByteOrderMarks[2]))
        return encoding.GetString(bytes.Skip(3).ToArray());

      if (bytes.Take(2).ContainsOnly(ByteOrderMarks[3]) || bytes.Take(2).ContainsOnly(ByteOrderMarks[4]))
        return encoding.GetString(bytes.Skip(2).ToArray());

      return content;
    }

    #endregion

    #endregion
  }

  public class CompareOptions
  {
    public int ContextSize { get; set; }
    public BomMode BomMode { get; set; }

    public CompareOptions()
    {
      ContextSize = 3;
      BomMode = BomMode.Include;
    }
  }
}