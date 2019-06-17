using System;
using System.Collections.Generic;

namespace Mastermind.Core.Models
{
  public class CodePiece : IEquatable<CodePiece>
  {
    public char Value { get; set; }
    public int Index { get; set; }

    public override bool Equals(object obj)
    {
      return Equals(obj as CodePiece);
    }

    public static IEnumerable<CodePiece> DecipherCode(string code)
    {
      for (int i = 0; i < code?.Length; i++)
      {
        yield return new CodePiece() { Value = code[i], Index = i };
      }
    }

    public bool Equals(CodePiece codePiece)
    {
      if (codePiece is null)
      {
        return false;
      }

      if (ReferenceEquals(this, codePiece))
      {
        return true;
      }

      if (GetType() != codePiece.GetType())
      {
        return false;
      }

      return (Value == codePiece.Value && Index == codePiece.Index);
    }

    public override int GetHashCode()
    {
      return Value ^ Index;
    }

    public static bool operator ==(CodePiece c, CodePiece p)
    {
      if (c is null && p is null)
      {
        return true;
      }

      return c.Equals(p);
    }

    public static bool operator !=(CodePiece c, CodePiece p)
    {
      return !(c == p);
    }
  }
}
