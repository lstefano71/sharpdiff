using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public interface ISnippet
    {
        IEnumerable<ILine> OriginalLines { get; }
        IEnumerable<ILine> ModifiedLines { get; }
    }
}