using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Subtraction : Expression
  {
    public string name => "sub";
    public string[] shorthands => new string[] { "-", "subtraction" };
    public string helpMessage => "Subtracts all arguments together and returns the result.";

    public string RunExpression(params string[] args)
    {
      double? returnNumber = null;
      foreach (string arg in args)
      {
        if (returnNumber == null)
        {
          returnNumber = double.Parse(arg);
          continue;
        }
        returnNumber -= double.Parse(arg);
      }
      Console.WriteLine(returnNumber);
      return returnNumber.ToString();
    }
  }
}
