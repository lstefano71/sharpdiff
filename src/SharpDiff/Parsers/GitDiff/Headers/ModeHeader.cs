namespace SharpDiff.Parsers.GitDiff {
    public class ModeHeader : IHeader {
        public ModeHeader(string kind, int mode) {
            Kind = kind;
            Mode = mode;
        }

    public string Kind { get; }
    public int Mode { get; }
  }
}
