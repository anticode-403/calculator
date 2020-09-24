using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions
{
  class Help : Expression
  {
    public string name => "help";
    public string[] shorthands => new string[] { "?" };
    public string helpMessage => "Prints the help message of other commands using help [command], or lists all commands at once.";

    public string RunExpression(params string[] args)
    {
      if (args.Length > 0)
      {
        string message = "";
        foreach (string arg in args)
        {
          Expression command = ExpressionParser.ParseExpression(arg);
          message += $"{command.name} - {command.helpMessage}\n";
        }
        string returnMessage = message.Trim();
        Console.WriteLine(returnMessage);
        return returnMessage;
      } else
      {
        string message = "";
        foreach (Expression command in ExpressionParser.expressionList)
        {
          message += $"{command.name} - {command.helpMessage}\n";
        }
        string returnMessage = message.Trim();
        Console.WriteLine(returnMessage);
        return returnMessage;
      }
    }
  }
}
