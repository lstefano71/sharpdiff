using System;
using System.Collections.Generic;
using SharpDiff.FileStructure;

namespace SharpDiff.FileStructure
{
    public class DiffHeader
    {
        public DiffHeader(FormatType format)
            : this(format, new List<IFile>()) { }

        public DiffHeader(FormatType format, string rawFileDefs)
            : this(format, ParseRawFileDefs(rawFileDefs)) { }

        public DiffHeader(FormatType format, IEnumerable<IFile> files)
        {
            Format = format;
            Files = new List<IFile>(files);
        }

        public static IEnumerable<IFile> ParseRawFileDefs(string rawFileDefs) {
            // TODO
            return new IFile[0];
        }

        public FormatType Format { get; private set; }
        public IList<IFile> Files { get; private set; }

        public bool IsNewFile
        {
            get { return Files[0] is NullFile; }
        }

        public bool IsDeletion
        {
            get { return Files[1] is NullFile; }
        }
    }
}