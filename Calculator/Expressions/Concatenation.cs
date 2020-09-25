using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Concatenation : Expression
  {
    public string name => "concat";
    public string[] shorthands => new string[] { "|", "concatenation" };
    public string helpMessage => "Concatenates all arguments into a singular value.";

    public string RunExpression(params string[] args)
    {
      string concatenatedString = "";
      foreach (string arg in args)
      {
        concatenatedString += arg;
      }
      Console.WriteLine(concatenatedString);
      return concatenatedString;
    }
  }
}
