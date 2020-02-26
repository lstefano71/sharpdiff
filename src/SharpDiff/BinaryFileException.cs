using System;

namespace SharpDiff
{
  [Serializable]
  public class BinaryFileException : Exception
    {
        public BinaryFileException(string path)
            : base("Could not compare binary file '" + path + "'")
        {}

    public BinaryFileException()
    {
    }

    public BinaryFileException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}