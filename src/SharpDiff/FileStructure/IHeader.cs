namespace SharpDiff.FileStructure {
    public interface IHeader {
    }

    public class ModeHeader : IHeader {
        public ModeHeader(string kind, int mode) {
            this.Kind = kind;
            this.Mode = mode;
        }
        
        public string Kind { get; private set; }
        public int Mode { get; private set; }
    }

    public class CopyRenameHeader : IHeader {
        public CopyRenameHeader(string transaction, string direction) {
            this.Transaction = transaction;
            this.Direction = direction;
        }

        public string Transaction { get; private set; }
        public string Direction { get; private set; }
    }

    public class SimilarityHeader : IHeader {
        public SimilarityHeader(string kind, int index) {
            this.Kind = kind;
            this.Index = index;
        }

        public string Kind { get; private set; }
        public int Index { get; private set; }
    }

    public class IndexHeader : IHeader {
        public IndexHeader(HashRange range, int? mode) {
            this.Range = range;
            this.Mode = mode;
        }

        public HashRange Range { get; private set; }
        public int? Mode { get; private set; }
    }
}
