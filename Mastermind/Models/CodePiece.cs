using System;

namespace Mastermind.Models
{
  public class CodePiece : IEquatable<CodePiece>
  {
    public char Value { get; set; }
    public int Index { get; set; }
    public bool Matched { get; set; } = false;

    public override bool Equals(object obj)
    {
      return this.Equals(obj as CodePiece);
    }

    public bool Equals(CodePiece codePiece)
    {
      if ( ReferenceEquals(codePiece, null))
      {
        return false;
      }

      if ( ReferenceEquals(this, codePiece))
      {
        return true;
      }

      if ( GetType() != codePiece.GetType())
      {
        return false;
      }

      return (Value == codePiece.Value && Index == codePiece.Index);
    }

    public override int GetHashCode()
    {
      return (Value + Index).GetHashCode();
    }

    public static bool operator ==(CodePiece c, CodePiece p)
    {
      if ( ReferenceEquals(c, null))
      {
        if ( ReferenceEquals(p, null))
        {
          return true;
        }
      }
      return c.Equals(p);
    }

    public static bool operator !=(CodePiece c, CodePiece p)
    {
      return !(c == p);
    }
  }
}
