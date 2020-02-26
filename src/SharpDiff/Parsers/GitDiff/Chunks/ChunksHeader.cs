namespace SharpDiff.Parsers.GitDiff
{
    public class ChunksHeader
    {
        public ChunksHeader(IFile originalFile, IFile newFile)
        {
            OriginalFile = originalFile;
            NewFile = newFile;
        }

    public IFile OriginalFile { get; }
    public IFile NewFile { get; }
  }
}