using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
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
      List<Expression> expressions = new List<Expression>();
      foreach (Type expressionType in
        Assembly.GetAssembly(typeof(Expression)).GetTypes()
        .Where(myExpression => myExpression.GetInterfaces().Contains(typeof(Expression)) && !myExpression.IsInterface))
      {
        Expression expression = (Expression)Activator.CreateInstance(expressionType, constructorArgs);
        expressions.Add(expression);
      }
      expressionList = expressions;
      return expressions;
    }

    class Token
    {
      public string data;

      public Token(string _data)
      {
        data = _data;
      }

      public override string ToString()
      {
        return data;
      }
    }

    class Scanner
    {
      private Token buffered;
      private string inputString;
      private int at = 0;

      public Scanner(string input)
      {
        inputString = input;
        buffered = Next();
      }
      
      public Token Get()
      {
        return buffered;
      }

      public void Advance()
      {
        buffered = Next();
      }

      private Token Next()
      {
        // Don't continue past end of string.
        if (at >= inputString.Length) return null;
        char ch = inputString[at];
        // Skip white space to get to the actual token.
        while (char.IsWhiteSpace(ch) && at < inputString.Length)
        {
          at++;
          ch = inputString[at];
        }
        // Don't continue past end of string after whitespace, but before token.
        if (at >= inputString.Length) return null;
        // Parens are their own tokens.
        if (ch == '(' || ch == ')')
        {
          at++;
          return new Token(ch.ToString());
        }

        // Evaluate strings as tokens of their own, and skip whitespace inside them.
        if (ch == '"')
        {
          for (int end = at + 1; end < inputString.Length; end++)
          {
            ch = inputString[end];
            // TODO: Evaluate other types of escaping.
            if (ch == '"' && inputString[end - 1] != '\\')
            {
              // For some unkown reason, Visual Studio throws an error if these tokens are the same name as the last token case.
              // This should be investigated more thoroughly.
              Token _token = new Token(inputString.Substring(at + 1, end - at + 1));
              at = end + 1;
              return _token;
            }
          }
        }

        // Evaluate other non-string values.
        for (int end = at + 1; end < inputString.Length; end++)
        {
          ch = inputString[end];
          if (ch == '(' || ch == ')' || char.IsWhiteSpace(ch))
          {
            Token _token = new Token(inputString.Substring(at, end - at));
            at = end;
            return _token;
          }
        }

        // I don't know when this state would ever be reached.
        Console.WriteLine("Unknown state reached.");
        Token token = new Token(inputString.Substring(at));
        at = inputString.Length;
        return token;
      }
    }

    class Tree
    {
      public Token op;
      public Tree[] operands;

      public Tree(Scanner scanner)
      {
        Token _op = scanner.Get();
        List<Tree> _operands = new List<Tree>();
        if (_op == null)
        {
          throw new Exception("No operator was fetched from provided Scanner.");
        }
        if (_op.data == "(")
        {
          scanner.Advance();
          _op = scanner.Get();
          scanner.Advance();
          while (scanner.Get().data != ")")
          {
            _operands.Add(new Tree(scanner));
          }
          Token confirmCloseParen = scanner.Get();
          if (confirmCloseParen == null || confirmCloseParen.data != ")")
          {
            throw new Exception("I literally do not understand");
          }
          scanner.Advance();
        }
        else scanner.Advance();
        op = _op;
        operands = _operands.ToArray();
      }

      public override string ToString()
      {
        string operandsString = "";
        foreach (Tree tree in operands)
        {
          operandsString += tree.ToString() + ",";
        }
        return operands.Length > 0 ? $"Tree( {op} {{{operandsString}}})" : $"Tree( {op} )";
      }
    }
    
    /// <summary>
    /// Expects to recieve an entire input string, complete with expression names, arguments, and nested expressions.
    /// Will parse the entire input before running expressions in expanding order.
    /// </summary>
    /// <param name="fullExpression"></param>
    /// <returns>The return of the outward-most expression.</returns>
    public static string ParseAndEvaluateExpression(string input)
    {
      if (!input.StartsWith('(') || !input.EndsWith(')')) input = $"({input})";
      Tree tokenTree = new Tree(new Scanner(input));
      Console.WriteLine(tokenTree.ToString());
      return tokenTree.ToString();
//      foreach (Expression expression in expressionList)
//      {
//        if (expression.name == expressionName || Array.IndexOf(expression.shorthands, expressionName) != -1)
//        {
//          return expression;
//        }
//      }
//      Expression expression = ParseExpression(expressionPieces[0]);
//      expressionPieces.RemoveAt(0);
//      return expression.RunExpression(expressionPieces.ToArray()).ToString();
    }
  }
}
