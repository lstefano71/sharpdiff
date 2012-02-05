using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class SubtractionLine : ILine
    {
        public SubtractionLine(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public IEnumerable<LineSpan> Spans
        {
            get { return new[] { new LineSpan(Value, LineSpanKind.Equal) }; }
        }
    }
}