using System.Collections.Generic;

namespace SharpDiff.FileStructure
{
    public class Diff
    {
        private readonly DiffHeader diffHeader;
        private readonly IEnumerable<IHeader> headers;

        public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers, IEnumerable<Chunk> chunks)
        {
            this.diffHeader = diffHeader;
            this.headers = headers;
            Chunks = new List<Chunk>(chunks);
        }

        public Diff(DiffHeader diffHeader, IEnumerable<Chunk> chunks)
            : this(diffHeader, new IHeader[0], chunks) {}

        public IList<Chunk> Chunks { get; private set; }

        public IList<IFile> Files
        {
            get { return diffHeader.Files; }
        }

        public bool IsNewFile
        {
            get { return diffHeader.IsNewFile; }
        }

        public bool IsDeletion
        {
            get { return diffHeader.IsDeletion; }
        }
    }
}