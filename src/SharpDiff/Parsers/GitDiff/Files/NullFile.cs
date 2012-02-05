namespace SharpDiff.Parsers.GitDiff
{
    public class NullFile : IFile
    {
        public string FileName
        {
            get { return "/dev/null"; }
        }
    }
}