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
    public static List<Command> commandList { get; private set; } = new List<Command>();

    public static List<Command> GetCommands(params object[] constructorArgs)
    {
      List<Command> commands = new List<Command>();
      foreach (Type commandType in
        Assembly.GetAssembly(typeof(Command)).GetTypes()
        .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && !myCommand.IsInterface && myCommand.IsSubclassOf(typeof(Command))))
      {
        Command command = (Command)Activator.CreateInstance(commandType, constructorArgs);
        commands.Add(command);
      }
      commandList = commands;
      return commands;
    }

    public static Command ParseCommand(string commandName)
    {
      foreach (Command cmd in commandList) if (cmd.name == commandName | Array.IndexOf(cmd.shorthands, commandName) != -1) return cmd;
      throw new Exception($"No command has the name or shorthand {commandName}.");
    }
  }
}
