namespace My.Utils
{
  using System;

  /// <summary>details of one difference.</summary>
  public struct Item : IEquatable<Item>
  {
    /// <summary>Start Line number in Data A.</summary>
    public int StartA { get; set; }
    /// <summary>Start Line number in Data B.</summary>
    public int StartB { get; set; }

    /// <summary>Number of changes in Data A.</summary>
    public int DeletedA { get; set; }
    /// <summary>Number of changes in Data B.</summary>
    public int DeletedB { get; set; }

    public override bool Equals(object obj)
    {
      return obj is Item item && Equals(item);
    }

    public bool Equals(Item other)
    {
      return StartA == other.StartA &&
             StartB == other.StartB &&
             DeletedA == other.DeletedA &&
             DeletedB == other.DeletedB;
    }

    public override int GetHashCode()
    {
      var hashCode = 643693650;
      hashCode = hashCode * -1521134295 + StartA.GetHashCode();
      hashCode = hashCode * -1521134295 + StartB.GetHashCode();
      hashCode = hashCode * -1521134295 + DeletedA.GetHashCode();
      hashCode = hashCode * -1521134295 + DeletedB.GetHashCode();
      return hashCode;
    }

    public static bool operator ==(Item left, Item right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Item left, Item right)
    {
      return !(left == right);
    }
  } // Item
} // namespace