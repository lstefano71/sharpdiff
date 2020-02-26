using System;
using System.IO;
using OMetaSharp;

namespace SharpDiff.Utils.RebuildParser
{
    class Program
    {
        static void Main()
        {
            var generator = new Program();

            generator.RebuildGitParser();
            generator.RebuildGitNumstatParser();
        }

        public void RebuildGitParser() {
            var contents = File.ReadAllText(@"..\..\..\SharpDiff\Parsers\GitDiff\GitDiffParser.ometacs");
            var result = Grammars.ParseGrammarThenOptimizeThenTranslate
                <OMetaParser, OMetaOptimizer, OMetaTranslator>
                (contents,
                 p => p.Grammar,
                 o => o.OptimizeGrammar,
                 t => t.Trans);

            File.WriteAllText(@"..\..\..\SharpDiff\Parsers\GitDiff\GitDiffParser.cs", result);
        }

        public void RebuildGitNumstatParser() {
            var contents = File.ReadAllText(@"..\..\..\SharpDiff\Parsers\GitNumstat\GitNumstatParser.ometacs");
            var result = Grammars.ParseGrammarThenOptimizeThenTranslate
                <OMetaParser, OMetaOptimizer, OMetaTranslator>
                (contents,
                 p => p.Grammar,
                 o => o.OptimizeGrammar,
                 t => t.Trans);

            File.WriteAllText(@"..\..\..\SharpDiff\Parsers\GitNumstat\GitNumstatParser.cs", result);
        }
    }
}