namespace SharpDiff.Parsers.GitDiff
{
    public class File : IFile
    {
        public File(char letter, string filename)
        {
            Letter = letter;
            FileName = filename;
        }

    public char Letter { get; }
    public string FileName { get; }
  }
}