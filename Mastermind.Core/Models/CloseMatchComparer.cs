using System.Collections.Generic;

namespace Mastermind.Core.Models
{
  public class CloseMatchComparer : IEqualityComparer<CodePiece>
  {
    public bool Equals(CodePiece x, CodePiece y)
    {
      if (ReferenceEquals(x, y)) { return true; }
      if (x is null || y is null) { return false; }
      return x.Value == y.Value;
    }

    public int GetHashCode(CodePiece obj)
    {
      if (obj is null) { return 0; }

      return obj.Value.GetHashCode();
    }
  }
}
