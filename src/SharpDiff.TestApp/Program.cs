using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDiff;
using SharpDiff.Parsers.GitDiff;

namespace SharpDiff.TestApp {
    class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                Console.WriteLine("Usage: SharpDiff.TestApp.exe file.diff");
                return;
            }

            string fileName = args[0];
            string content = System.IO.File.ReadAllText(fileName);
            IEnumerable<Diff> diffs = Differ.LoadGitDiffParallel(content);
            List<Diff> l = new List<Diff>(312);
            int diffCount = 0;
            foreach(var diff in diffs) {
                diffCount++;
                //l.Add(diff);
                Console.Write("+ {0} files", diff.Files.Count);
                if(diff.HasChunks) {
                    Console.WriteLine(", {0} chunks", diff.Chunks.Count);
                    foreach(var chunk in diff.Chunks) {
                        Console.WriteLine("  + {0} snippets", chunk.Snippets.Count());
                    }
                } else if(diff.IsBinary) {
                    Console.WriteLine(", binary");
                }
            }
            Console.WriteLine("{0} diffs", diffCount);
            Console.WriteLine("{0} diffs", l.Count);

            Console.ReadLine();
        }
    }
}
