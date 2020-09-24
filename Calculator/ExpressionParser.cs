using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Calculator
{
  public static class ExpressionParser
  {
    public static List<Expression> expressionList { get; private set; } = new List<Expression>();

    public static List<Expression> GetCommands(params object[] constructorArgs)
    {
      List<Expression> exressions = new List<Expression>();
      foreach (Type expressionType in
        Assembly.GetAssembly(typeof(Expression)).GetTypes()
        .Where(myExpression => myExpression.GetInterfaces().Contains(typeof(Expression)) && !myExpression.IsInterface))
      {
        Expression expression = (Expression)Activator.CreateInstance(expressionType, constructorArgs);
        exressions.Add(expression);
      }
      expressionList = exressions;
      return exressions;
    }

    public static Expression ParseExpression(string commandName)
    {
      foreach (Expression cmd in expressionList)
      {
        if (cmd.name == commandName || Array.IndexOf(cmd.shorthands, commandName) != -1)
        {
          return cmd;
        }
      }
      throw new Exception($"No command has the name or shorthand {commandName}.");
    }
  }
}
