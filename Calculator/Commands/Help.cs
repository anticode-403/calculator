using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Commands
{
  class Help : Expression
  {
    public string name => "help";
    public string[] shorthands => new string[] { "?" };
    public string helpMessage => "Prints the help message of other commands using help [command], or lists all commands at once.";

    public void RunExpression(params string[] args)
    {
      if (args.Length > 0)
      {
        foreach (string arg in args)
        {
          Expression command = ExpressionParser.ParseExpression(arg);
          Console.WriteLine(command.helpMessage);
        }
      } else
      {
        string message = "";
        foreach (Expression command in ExpressionParser.expressionList)
        {
          message += $"{command.name} - {command.helpMessage}\n";
        }
        Console.WriteLine(message);
      }
      throw new NotImplementedException();
    }
  }
}
