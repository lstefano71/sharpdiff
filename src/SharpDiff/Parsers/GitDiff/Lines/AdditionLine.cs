using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class AdditionLine : ILine
    {
        public AdditionLine(string value)
        {
            Value = value;
        }

    public string Value { get; }

    public IEnumerable<LineSpan> Spans
        {
            get { return new[] { new LineSpan(Value, LineSpanKind.Addition) }; }
        }
    }
}