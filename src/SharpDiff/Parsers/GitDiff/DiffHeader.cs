using System;
using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class DiffHeader
    {
        public DiffHeader(DiffFormatType format)
            : this(format, new List<IFile>()) { }

        public DiffHeader(DiffFormatType format, string rawFileDefs)
            : this(format, ParseRawFileDefs(rawFileDefs)) { }

        public DiffHeader(DiffFormatType format, IEnumerable<IFile> files)
        {
            Format = format;
            Files = new List<IFile>(files);
        }

        public static IEnumerable<IFile> ParseRawFileDefs(string rawFileDefs) {
            // TODO
            return new IFile[0];
        }

        public DiffFormatType Format { get; private set; }
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

    public class DiffFormatType {
        public DiffFormatType(string format) {
            this.Name = format;
        }

        public string Name { get; private set; }
    }
}