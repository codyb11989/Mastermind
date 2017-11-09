using Mastermind.Models;
using System.Collections.Generic;

namespace Mastermind.Helpers
{
  public class CodeComparer : IEqualityComparer<Code>
  {
    public bool Equals(Code x, Code y)
    {
      return x.Value == y.Value && x.Index == y.Index;
    }

    public int GetHashCode(Code obj)
    {
      return (obj.Value + obj.Index).GetHashCode();
    }
  }
}
