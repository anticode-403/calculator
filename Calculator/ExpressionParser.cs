using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Calculator
{
  public static class ExpressionParser
  {
    public static List<Expression> expressionList { get; private set; } = new List<Expression>();

    /// <summary>
    /// Searches the assembly for expressions. A costly operation that should only be run extremely rarely.
    /// </summary>
    /// <param name="constructorArgs"></param>
    /// <returns>A list of all expressions.</returns>
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

    /// <summary>
    /// Expects to recieve an expression name in the form of a string. ParseExpression only expects to read an expression name, and nothing more.
    /// </summary>
    /// <param name="expressionName"></param>
    /// <returns>The first expression that uses that name or shorthand, if no named expression is found.</returns>
    public static Expression ParseExpression(string expressionName)
    {
      expressionName = expressionName.Trim(new char[] { '(', ')' });
      foreach (Expression expression in expressionList)
      {
        if (expression.name == expressionName || Array.IndexOf(expression.shorthands, expressionName) != -1)
        {
          return expression;
        }
      }
      throw new Exception($"No command has the name or shorthand {expressionName}.");
    }
    
    /// <summary>
    /// Expects to recieve an entire input string, complete with expression names, arguments, and nested expressions.
    /// Will parse the entire input before running expressions in expanding order.
    /// </summary>
    /// <param name="fullExpression"></param>
    /// <returns>The return of the outward-most expression.</returns>
    public static string ParseAndEvaluateExpression(string input)
    {
      // Remove the first opening paren.
      if (input.StartsWith('(')) input.Substring(1);
      List<string> expressionPieces = new List<string>();
      string currentString = "";
      char lastImportantChar = ' ';
      foreach (char currentCharacter in input)
      {
        if (currentCharacter == '(' && !currentString.StartsWith('"'))
        {
          currentString += currentCharacter;
          lastImportantChar = currentCharacter;
        }
        else if (currentCharacter == ' ' && !currentString.StartsWith('"') && currentString.Length > 0 && lastImportantChar != '(')
        {
          expressionPieces.Add(currentString);
          currentString = "";
          lastImportantChar = ' ';
        }
        else if (currentCharacter == ')' && !currentString.StartsWith('"'))
        {
          if (lastImportantChar != '(')
          {
            expressionPieces.Add(currentString);
            break;
          }
          lastImportantChar = currentCharacter;
          currentString += currentCharacter;
          expressionPieces.Add(ParseAndEvaluateExpression(currentString));
          currentString = "";
        }
        else if (currentCharacter == '"' && currentString.StartsWith('"'))
        {
          currentString += currentCharacter;
          expressionPieces.Add(currentString);
          currentString = "";
          lastImportantChar = ' ';
        }
        else
        {
          currentString += currentCharacter;
        }
      }
      if (currentString.Length > 0) expressionPieces.Add(currentString);
      if (lastImportantChar == '(')
      {
        throw new Exception("Opening paren did not have a closing paren, and was not in quotations.");
      }
      Expression expression = ParseExpression(expressionPieces[0]);
      expressionPieces.RemoveAt(0);
      if (expressionPieces.Count > 0)
      {
        return expression.RunExpression(expressionPieces.ToArray()).ToString();
      }
      else
      {
        return expression.RunExpression().ToString();
      }
    }
  }
}
