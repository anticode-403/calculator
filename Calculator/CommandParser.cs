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
    public static Dictionary<string, Command> commandDictionary { get; private set; } = new Dictionary<string, Command>();

    public static Dictionary<string, Command> GetCommands(params object[] constructorArgs)
    {
      Dictionary<string, Command> commands = new Dictionary<string, Command>();
      foreach (Type commandType in
        Assembly.GetAssembly(typeof(Command)).GetTypes()
        .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && !myCommand.IsInterface && myCommand.IsSubclassOf(typeof(Command))))
      {
        Command command = (Command)Activator.CreateInstance(commandType, constructorArgs);
        commands.Add(command.name, command);
      }
      commandDictionary = commands;
      return commands;
    }

    public static Command ParseCommand(string commandName)
    {
      Command command = null;
      try
      {
        command = commandDictionary[commandName];
        return command;
      } catch
      {
        foreach (Command cmd in commandDictionary.Values)
        {
          if (Array.Exists(cmd.shorthands, shorthand => commandName == shorthand))
          {
            command = cmd;
            return command;
          }
        }
        throw new Exception($"No command has the name or shorthand {commandName}.");
      }
    }
  }
}
