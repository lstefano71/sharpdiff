using System;
using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class DiffHeader
    {
        protected string rawFileDefs;
        internal string RawFileDefs {
            get { return this.rawFileDefs; }
        }

        protected Diff diff; // might be required to determine the file names.
        internal Diff Diff {
            set { this.diff = value; }
        }

        protected IList<IFile> files;

        public DiffHeader(DiffFormatType format)
            : this(format, new IFile[0]) { }

        public DiffHeader(DiffFormatType format, string rawFileDefs)
            : this(format, new IFile[0]) {
            this.rawFileDefs = rawFileDefs;
        }

        public DiffHeader(DiffFormatType format, IEnumerable<IFile> files) {
            Format = format;
            files = new List<IFile>(files);
        }

        public DiffFormatType Format { get; private set; }
        public IList<IFile> Files {
            get {
                if(this.files == null) {
                    this.diff.DetermineFileNames();
                }
                return this.files;
            }

            internal set {
                this.files = value;
            }
        }

        public bool IsNewFile {
            get { return Files[0] is NullFile; }
        }

        public bool IsDeletion {
            get { return Files[1] is NullFile; }
        }
    }

    public class DiffFormatType {
        public DiffFormatType(string format) {
            this.Name = format;
        }

        public string Name { get; private set; }
    }
}