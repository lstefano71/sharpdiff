using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public interface ILine
    {
        string Value { get; }
        IEnumerable<LineSpan> Spans { get; }
    }
}