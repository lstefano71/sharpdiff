using System;
using System.IO;
using OMetaSharp;

namespace SharpDiff
{
    public class OMetaCodeGenerator
    {
        public void Rebuild()
        {
            var contents = File.ReadAllText("Parser\\DiffParser.ometacs");
            var result = Grammars.ParseGrammarThenOptimizeThenTranslate
                <OMetaParser, OMetaOptimizer, OMetaTranslator>
            (contents,
                p => p.Grammar,
                o => o.OptimizeGrammar,
                t => t.Trans);

            File.WriteAllText(@"C:\Development\SharpDiff\SharpDiff\Parser\DiffParser.cs", result);
        }
    }
}