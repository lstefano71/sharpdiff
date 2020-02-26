using System.Collections.Generic;
using System.Linq;

namespace SharpDiff.Parsers.GitDiff {

    public class BinaryFiles {
    internal string RawFileDefs { get; }

    Diff diff; // might be required to determine the file names. See DetermineFiles().
        internal Diff Diff { set { diff = value; } }

        public IList<IFile> Files { get { return diff.Files; } }

        public BinaryFiles(string rawFileDefs) {
            RawFileDefs = rawFileDefs;
        }
    }
}
