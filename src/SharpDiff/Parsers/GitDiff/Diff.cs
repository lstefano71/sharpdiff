using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public partial class Diff
    {
        protected DiffHeader diffHeader;
        protected IEnumerable<IHeader> headers;

        protected IList<IFile> files; // in contrast to the diff header's files, this collection may contain a NullFile

        protected ChunksHeader chunksHeader;
        protected IList<Chunk> chunks;

        protected BinaryFiles binaryFiles;

        public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers) {
            this.diffHeader = diffHeader;
            this.diffHeader.Diff = this; // might help to parse the file names
            this.headers = headers;
        }

        public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers, ChunksHeader chunksHeader, IEnumerable<Chunk> chunks)
            : this(diffHeader, headers) {
            this.chunksHeader = chunksHeader;
            if(chunks != null) this.chunks = new List<Chunk>(chunks);
        }

        public Diff(DiffHeader diffHeader, IEnumerable<IHeader> headers, BinaryFiles binaryFiles)
            : this(diffHeader, headers) {
            this.binaryFiles = binaryFiles;
            this.binaryFiles.Diff = this; // might help to parse the file names
        }

        public IList<Chunk> Chunks { get { return this.chunks; } }

        public IList<IFile> Files {
            get {
                if(this.files == null) {
                    this.DetermineFileNames();
                }
                return this.files;
            }
        }
        
        public bool IsNewFile {
            get { return diffHeader.IsNewFile; }
        }
        public bool IsDeletion {
            get { return diffHeader.IsDeletion; }
        }

        public bool HasChunks {
            get { return this.chunks != null; }
        }
        public bool IsBinary {
            get { return this.binaryFiles != null; }
        }

        public bool GetCopyRenameHeaders(out CopyRenameHeader from, out CopyRenameHeader to) {
            from = null; to = null;
            foreach(var header in this.headers) {
                if(header is CopyRenameHeader) {
                    CopyRenameHeader header_ = (CopyRenameHeader)header;
                    if(header_.Direction == "from") from = header_;
                    else if(header_.Direction == "to") to = header_;

                    if(from != null && to != null) return true;
                }
            }

            return false;
        }
    }
}