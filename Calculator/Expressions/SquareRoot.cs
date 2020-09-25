using System;
using System.Collections.Generic;

namespace Calculator.Expressions
{
  class SquareRoot : Expression
  {
    public string name => "sqrt";
    public string[] shorthands => new string[] { "square_root" };
    public string helpMessage => "Find the square root of a singular argument.";

    public string RunExpression(params string[] args)
    {
      if (args.Length > 0)
      {
        throw new Exception("The SquareRoot command was given more than one argument.");
      }
      double root = MathF.Sqrt(float.Parse(args[0]));
      Console.WriteLine(root);
      return root.ToString();
    }
  }
}
