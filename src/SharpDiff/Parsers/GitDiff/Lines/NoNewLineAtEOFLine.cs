using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class NoNewLineAtEOFLine : ILine
    {
        public string Value
        {
            get { return "No newline at end of file"; }
        }

        public IEnumerable<LineSpan> Spans
        {
            get { return new[] { new LineSpan(Value, LineSpanKind.Equal) }; }
        }
    }
}