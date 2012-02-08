using System.Collections.Generic;
using System.Linq;

namespace SharpDiff.Parsers.GitDiff {

    public class BinaryFiles {

        protected string rawFileDefs;
        internal string RawFileDefs { get { return this.rawFileDefs; } }

        protected Diff diff; // might be required to determine the file names. See DetermineFiles().
        internal Diff Diff { set { this.diff = value; } }

        public IList<IFile> Files { get { return this.diff.Files; } }

        public BinaryFiles(string rawFileDefs) {
            this.rawFileDefs = rawFileDefs;
        }
    }
}
