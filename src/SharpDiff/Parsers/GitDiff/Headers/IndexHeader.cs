namespace SharpDiff.Parsers.GitDiff {
    public class IndexHeader : IHeader {
        public IndexHeader(HashRange range, int? mode) {
            this.Range = range;
            this.Mode = mode;
        }

    public HashRange Range { get; }
    public int? Mode { get; private set; }
    }

    public class HashRange {
        public HashRange(string start, string end) {
            Start = start;
            End = end;
        }

    public string Start { get; }
    public string End { get; }
  }
}
