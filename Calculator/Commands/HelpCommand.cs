using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Commands
{
  class HelpCommand : Command
  {
    public string name => "help";
    public string[] shorthands => new string[] { "?" };
    public string helpMessage => "Prints the help message of other commands using help [command], or lists all commands at once.";

    public void RunCommand(params string[] args)
    {
      if (args.Length > 0)
      {
        foreach (string arg in args)
        {
          Command command = CommandParser.ParseCommand(arg);
          Console.WriteLine(command.helpMessage);
        }
      } else
      {
        string message = "";
        foreach (Command command in CommandParser.commandList)
        {
          message += $"{command.name} - {command.helpMessage}\n";
        }
        Console.WriteLine(message);
      }
      throw new NotImplementedException();
    }
  }
}
