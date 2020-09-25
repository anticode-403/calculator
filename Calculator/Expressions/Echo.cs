using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Echo : Expression
  {
    public string name => "echo";
    public string[] shorthands => new string[] {  };
    public string helpMessage => "Echo all the arguments as a singular string.";

    public string RunExpression(params string[] args)
    {
      string echoString = String.Join(' ', args);
      Console.WriteLine(echoString);
      return echoString;
    }
  }
}
