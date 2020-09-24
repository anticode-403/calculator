using System;

namespace Calculator
{
  class Calculator
  {
    static void Main()
    {
      Console.WriteLine("Calculator awaiting input...");
      Console.WriteLine("You can type 'help' to get all commands.");
      ExpressionParser.GetCommands();
      MainLoop();
    }

    static void MainLoop()
    {
      string input = Console.ReadLine();
      ExpressionParser.ParseExpression(input).RunExpression();
      MainLoop();
    }
  }
}
