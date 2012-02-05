using System;

namespace SharpDiff.Parsers.GitDiff {

    public enum LineSpanKind {
        Equal,
        Addition,
        Deletion
    }

    public class LineSpan {

        public LineSpan(string value, LineSpanKind kind)
        {
            Value = value;
            Kind = kind;
        }

        public string Value { get; private set; }
        public LineSpanKind Kind { get; private set; }
    }
}