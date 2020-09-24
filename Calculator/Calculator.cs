using System;

namespace Calculator
{
  class Calculator
  {
    static void Main()
    {
      ExpressionParser.GetCommands();
      Console.WriteLine("Calculator awaiting input...");
      Console.WriteLine("You can type 'help' to get all commands.");
      MainLoop();
    }

    static void MainLoop()
    {
      string input = Console.ReadLine();
      ExpressionParser.ParseAndEvaluateExpression(input);
      MainLoop();
    }
  }
}
