namespace SharpDiff.FileStructure {
    public class BinaryFiles {
        public BinaryFiles(string rawFileDefs) {
            // TODO
        }

        public BinaryFiles(IFile file1, IFile file2) {
            this.File1 = file1;
            this.File2 = file2;
        }

        public IFile File1 { get; private set; }
        public IFile File2 { get; private set; }
    }
}
