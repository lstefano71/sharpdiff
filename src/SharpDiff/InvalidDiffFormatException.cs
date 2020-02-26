using System;

namespace SharpDiff
{
  [Serializable]
  public class InvalidDiffFormatException : Exception
    {
        public InvalidDiffFormatException()
            : base("Invalid diff format supplied.")
        {}

    public InvalidDiffFormatException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InvalidDiffFormatException(string message) : base(message)
    {
    }
  }
}