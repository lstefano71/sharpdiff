using System;
using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class DiffHeader
    {

    internal string RawFileDefs { get; }

    Diff diff; // might be required to determine the file names.

        internal Diff Diff {
            set { this.diff = value; }
        }

        IList<IFile> files;

        public DiffHeader(DiffFormatType format)
            : this(format, Array.Empty<IFile>()) { }

        public DiffHeader(DiffFormatType format, string rawFileDefs)
            : this(format, Array.Empty<IFile>()) {
            this.RawFileDefs = rawFileDefs;
        }

        public DiffHeader(DiffFormatType format, IEnumerable<IFile> files) {
            Format = format;
            files = new List<IFile>(files);
        }

    public DiffFormatType Format { get; }

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

    public string Name { get; }
  }
}