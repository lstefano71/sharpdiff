namespace SharpDiff.Parsers.GitDiff {
   public class SimilarityHeader : IHeader {
        public SimilarityHeader(string kind, int index) {
            this.Kind = kind;
            this.Index = index;
        }

        public string Kind { get; private set; }
        public int Index { get; private set; }
    }
}
