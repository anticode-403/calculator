using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Multiplication : Expression
  {
    public string name => "multiply";
    public string[] shorthands => new string[] { "*", "multiplication" };
    public string helpMessage => "Multiplies all arguments together and returns the result.";

    public string RunExpression(params string[] args)
    {
      double returnNumber = 0;
      foreach (string arg in args)
      {
        returnNumber *= double.Parse(arg);
      }
      Console.WriteLine(returnNumber);
      return returnNumber.ToString();
    }
  }
}
