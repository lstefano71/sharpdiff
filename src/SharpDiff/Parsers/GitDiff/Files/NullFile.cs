namespace SharpDiff.Parsers.GitDiff
{
    public class NullFile : IFile
    {
        public const string NAME = "/dev/null";

        public string FileName
        {
            get { return NAME; }
        }
    }
}