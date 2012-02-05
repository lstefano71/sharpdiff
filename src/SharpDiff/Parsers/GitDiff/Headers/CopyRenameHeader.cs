namespace SharpDiff.Parsers.GitDiff {
    public class CopyRenameHeader : IHeader {
        public CopyRenameHeader(string transaction, string direction, string fileName) {
            this.Transaction = transaction;
            this.Direction = direction;
            this.FileName = fileName;
        }

        public string Transaction { get; private set; }
        public string Direction { get; private set; }
        public string FileName { get; private set; }
    }
}
