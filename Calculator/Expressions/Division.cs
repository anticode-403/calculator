using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Division : Expression
  {
    public string name => "divide";
    public string[] shorthands => new string[] { "/", "division" };
    public string helpMessage => "Divides two arguments and returns the result.";

    public string RunExpression(params string[] args)
    {
      if (args.Length > 2)
      {
        throw new Exception("The Division command was passed more than two arguments.");
      }
      double denominator = double.Parse(args[0]);
      double quotient = denominator / double.Parse(args[1]);
      Console.WriteLine(quotient);
      return quotient.ToString();
    }
  }
}
