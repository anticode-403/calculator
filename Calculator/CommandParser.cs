using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Calculator
{
  public static class CommandParser
  {
    private static Dictionary<string, Command> _commands = new Dictionary<string, Command>();

    public static Dictionary<string, Command> GetCommands(params object[] constructorArgs)
    {
      Dictionary<string, Command> commands = new Dictionary<string, Command>();
      foreach (Type commandType in
        Assembly.GetAssembly(typeof(Command)).GetTypes()
        .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && myCommand.IsSubclassOf(typeof(Command))))
      {
        Command command = (Command)Activator.CreateInstance(commandType, constructorArgs);
        commands.Add(command.name, command);
      }
      _commands = commands;
      return commands;
    }

    public static Command ParseCommand(string commandName)
    {
      Command command = _commands[commandName];
      return command;
    }
  }
}
