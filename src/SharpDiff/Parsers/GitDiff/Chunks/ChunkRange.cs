namespace SharpDiff.Parsers.GitDiff
{
    public class ChunkRange
    {
        public ChunkRange(ChangeRange originalRange, ChangeRange newRange)
        {
            OriginalRange = originalRange;
            NewRange = newRange;
        }

    public ChangeRange OriginalRange { get; }
    public ChangeRange NewRange { get; }
  }
}