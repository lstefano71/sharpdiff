using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDiff.Parsers.GitNumstat
{
    public class FileStats
    {
        public FileStats(int additions, int subtractions, string filename)
        {
            Additions = additions;
            Subtractions = subtractions;
            Filename = filename;
        }

    public int Additions { get; }
    public int Subtractions { get; }
    public string Filename { get; }
  }
}
