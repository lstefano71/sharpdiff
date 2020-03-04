using System;
using System.Collections.Generic;
using System.Linq;
using DiffMatchPatch;
using My.Utils;
using Chunk = DiffMatchPatch.Patch;
using Diff = My.Utils.Diff;
using Line = DiffMatchPatch.Diff;

namespace SharpDiff
{
  public class PatchMaker
  {
    public IEnumerable<DiffMatchPatch.Patch> MakePatch(string contentOne, string contentTwo)
    {
      return MakePatch(contentOne, contentTwo, 3);
    }

    public IEnumerable<DiffMatchPatch.Patch> MakePatch(string contentOne, string contentTwo, int contextSize)
    {
      return MakePatch(contentOne, contentTwo, new CompareOptions() { BomMode = BomMode.Ignore, ContextSize = contextSize });
    }


    public IEnumerable<DiffMatchPatch.Patch> MakePatch(string contentOne, string contentTwo, CompareOptions options)
    {
      var changes = My.Utils.Diff.DiffText(contentOne, contentTwo);
      var contentOneLines = contentOne.Split(new[] { "\n" }, StringSplitOptions.None);
      var contentTwoLines = contentTwo.Split(new[] { "\n" }, StringSplitOptions.None);

      var chunks = new HashSet<Chunk>();
      Chunk currentChunk = null;

      for (var i = 0; i < changes.Length; i++) {
        var change = changes[i];
        Item? nextChange = null;

        if (changes.Length > i + 1)
          nextChange = changes.ElementAtOrDefault(i + 1);

        var continuation = currentChunk != null;

        if (!continuation) {
          currentChunk = CreateChunk(change, options);
          chunks.Add(currentChunk);
        }

        if (change.StartA != 0 && !continuation) {
          // no start context needed
          currentChunk.Start1 = Math.Max(0,change.StartA - options.ContextSize);
          currentChunk.Start2 = Math.Max(0, change.StartB - options.ContextSize);

          // stick some context in
          var start = Math.Max(0, change.StartB - options.ContextSize);
          for (var j = start; j < change.StartB; j++) {
            currentChunk.Diffs.Add(new Line(Operation.EQUAL, contentTwoLines[j]));
          }
        }

        if (change.DeletedA > 0) {
          for (var j = 0; j < change.DeletedA; j++) {
            var line = contentOneLines[j + change.StartA];
            currentChunk.Diffs.Add(new Line(Operation.DELETE, line));
          }
        }

        if (change.DeletedB > 0) {
          for (var j = 0; j < change.DeletedB; j++) {
            var line = contentTwoLines[j + change.StartB];
            currentChunk.Diffs.Add(new Line(Operation.INSERT, line));
          }
        }

        var start2 = change.StartB + change.DeletedB;
        int end;

        if (nextChange.HasValue)
          end = Min(start2 + options.ContextSize, contentTwoLines.Length, nextChange.Value.StartB);
        else
          end = Min(start2 + options.ContextSize, contentTwoLines.Length);

        for (var j = start2; j < end; j++) {
          currentChunk.Diffs.Add(new Line(Operation.EQUAL, contentTwoLines[j]));
        }

        if (nextChange.HasValue && nextChange.Value.StartB - end > 0) {
          // need to split the diff into multiple chunks
          currentChunk = null;
        }
      }

      foreach (var chunk in chunks) {
        chunk.Length1 = chunk.Diffs.Count(x => x.Type == Operation.EQUAL || x.Type == Operation.DELETE);
        chunk.Length2 = chunk.Diffs.Count(x => x.Type == Operation.EQUAL || x.Type == Operation.INSERT);
      }

      return chunks;
    }

    private int Min(params int[] ints)
    {
      if (ints.Length == 1)
        return ints[0];

      var small = Math.Min(ints[0], ints[1]);

      return Min(new[] { small }.Concat(ints.Skip(2)).ToArray());
    }

    static private Chunk CreateChunk(Item change, CompareOptions options)
    {
      var chunk = new Chunk {
        Start1 = change.StartA,
        Start2 = change.StartB,
        Length1 = change.DeletedA + options.ContextSize,
        Length2 = change.DeletedB + options.ContextSize,
        Diffs = new List<Line>()
      };
      return chunk;
    }

    //private static int DistanceBetween(Item change, Item nextChange)
    //{
    //  return nextChange.StartB - change.StartB + change.DeletedB;
    //}
  }
}