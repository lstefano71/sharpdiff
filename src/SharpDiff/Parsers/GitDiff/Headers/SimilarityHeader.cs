namespace SharpDiff.Parsers.GitDiff {
   public class SimilarityHeader : IHeader {
        public SimilarityHeader(string kind, int index) {
            Kind = kind;
            Index = index;
        }

    public string Kind { get; }
    public int Index { get; }
  }
}
