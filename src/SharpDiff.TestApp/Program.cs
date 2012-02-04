using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDiff;
using SharpDiff.FileStructure;

namespace SharpDiff.TestApp {
    class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                Console.WriteLine("Usage: SharpDiff.TestApp.exe file.diff");
                return;
            }

            string fileName = args[0];
            string content = System.IO.File.ReadAllText(fileName);
            IEnumerable<Diff> diffs = Differ.Load(content);
            Console.WriteLine("{0} diffs", diffs.Count());

            foreach(var diff in diffs) {
                Console.WriteLine("{0} chunks", diff.Chunks.Count());
                foreach(var chunk in diff.Chunks) {
                    Console.WriteLine("  {0} snippets", chunk.Snippets.Count());
                }
            }

            Console.ReadLine();
        }
    }
}
