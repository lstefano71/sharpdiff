namespace SharpDiff.Parsers.GitDiff {
    public class ModeHeader : IHeader {
        public ModeHeader(string kind, int mode) {
            this.Kind = kind;
            this.Mode = mode;
        }
        
        public string Kind { get; private set; }
        public int Mode { get; private set; }
    }
}
