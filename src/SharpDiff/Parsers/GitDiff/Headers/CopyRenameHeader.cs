namespace SharpDiff.Parsers.GitDiff {
    public class CopyRenameHeader : IHeader {
        public CopyRenameHeader(string transaction, string direction, string fileName) {
            Transaction = transaction;
            Direction = direction;
            FileName = fileName;
        }

    public string Transaction { get; }
    public string Direction { get; }
    public string FileName { get; }
  }
}
