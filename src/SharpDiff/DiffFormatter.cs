
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpDiff.Parsers.GitDiff
{
  public partial class Diff
  {
    public string FormatAsUnifiedDiff()
    {
      using (var s = new MemoryStream()) {
        ExportAsUnifiedDiff(s);
        s.Close();
        return System.Text.Encoding.UTF8.GetString(s.ToArray());
      }
    }
    public void ExportAsUnifiedDiff(Stream output)
    {
      using (var txtout = new StreamWriter(output,new UTF8Encoding(false),1024,true)) {
        txtout.AutoFlush = false;
        txtout.WriteLine("--- " + chunksHeader.OriginalFile.FileName);
        txtout.WriteLine("+++ " + chunksHeader.NewFile.FileName);
        foreach(var c in Chunks) {
          txtout.WriteLine($"@@ -{c.OriginalRange.StartLine},{c.OriginalRange.LinesAffected} +{c.NewRange.StartLine},{c.NewRange.LinesAffected} @@");
          foreach(var e in c.Snippets) {
            foreach(var l in e.OriginalLines) {
              var p = PrefixForOriginal(l);
              txtout.Write(p);
              foreach(var s in l.Spans.Where(s => s.Kind==LineSpanKind.Equal || s.Kind== LineSpanKind.Deletion)) {
                txtout.Write(s.Value);
              }
              txtout.WriteLine();
            }
            foreach (var l in e.ModifiedLines) {
              var p = PrefixForModified(l);
              txtout.Write(p);
              foreach (var s in l.Spans.Where(s => s.Kind == LineSpanKind.Equal || s.Kind == LineSpanKind.Addition)) {
                txtout.Write(s.Value);
              }
              txtout.WriteLine();
            }
          }
          txtout.Flush();
        }
      }
    }

    static private string PrefixForOriginal(ILine l)
    {
      if (l is AdditionLine) return "+";
      if (l is ModificationLine) return "-";
      if (l is SubtractionLine) return "-";
      return " ";
    }
    static private string PrefixForModified(ILine l)
    {
      if (l is AdditionLine) return "+";
      if (l is ModificationLine) return "+";
      if (l is SubtractionLine) return "-";
      return " ";
    }
  }
}