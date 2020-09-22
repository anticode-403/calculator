using System;

namespace Calculator
{
  class Calculator
  {
    static void Main()
    {
      ExpressionParser.GetCommands();
      MainLoop();
    }

    static void MainLoop()
    {
      Console.WriteLine("Calculator awaiting input...");
      Console.WriteLine("You can type 'help' to get all commands.");
      string input = Console.ReadLine();
      ExpressionParser.ParseExpression(input).RunExpression();
      MainLoop();
    }
  }
}
