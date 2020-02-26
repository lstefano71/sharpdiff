using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
  public partial class Diff
  {
    readonly DiffHeader diffHeader;
    readonly IEnumerable<IHeader> headers;

    IList<IFile> files; // in contrast to the diff header's files, this collection may contain a NullFile

    readonly ChunksHeader chunksHeader;
    readonly IList<Chunk> chunks;

    readonly BinaryFiles binaryFiles;

    public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers)
    {
      this.diffHeader = diffHeader;
      this.diffHeader.Diff = this; // might help to parse the file names
      this.headers = headers;
    }

    public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers, ChunksHeader chunksHeader, IEnumerable<Chunk> chunks)
        : this(diffHeader, headers)
    {
      this.chunksHeader = chunksHeader;
      if (chunks != null) this.chunks = new List<Chunk>(chunks);
    }

    public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers, BinaryFiles binaryFiles)
        : this(diffHeader, headers)
    {
      this.binaryFiles = binaryFiles;
      this.binaryFiles.Diff = this; // might help to parse the file names
    }

    public IList<Chunk> Chunks { get { return chunks; } }

    public IList<IFile> Files
    {
      get {
        if (files == null) {
          DetermineFileNames();
        }
        return files;
      }
    }

    public bool IsNewFile
    {
      get { return diffHeader.IsNewFile; }
    }

    public bool IsDeletion
    {
      get { return diffHeader.IsDeletion; }
    }

    public bool HasChunks
    {
      get { return chunks != null; }
    }

    public bool IsBinary
    {
      get { return binaryFiles != null; }
    }

    public bool GetCopyRenameHeaders(out CopyRenameHeader from, out CopyRenameHeader to)
    {
      from = null; to = null;
      foreach (var header in headers) {
        if (header is CopyRenameHeader header_) {
          if (header_.Direction == "from") from = header_;
          else if (header_.Direction == "to") to = header_;

          if (from != null && to != null) return true;
        }
      }

      return false;
    }
  }
}