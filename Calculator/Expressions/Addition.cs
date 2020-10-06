using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Addition : Expression
  {
    public string name => "add";
    public string[] shorthands => new string[] { "+", "addition" };
    public string helpMessage => "Adds all arguments together and returns the result.";

    public string RunExpression(params string[] args)
    {
      // Theoretically this should work fine, but there's some unknown issue here.
      double returnNumber = 0;
      foreach (string arg in args)
      {
        returnNumber += double.Parse(arg);
      }
      Console.WriteLine(returnNumber);
      return returnNumber.ToString();
    }
  }
}
