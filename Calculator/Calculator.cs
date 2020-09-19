using System;

namespace Calculator
{
  class Calculator
  {
    static void Main()
    {
      CommandParser.GetCommands();
      MainLoop();
    }

    static void MainLoop()
    {
      Console.WriteLine("Calculator awaiting input...");
      Console.WriteLine("You can type 'help' to get all commands.");
      string input = System.Console.ReadLine();
      MainLoop();
    }
  }
}
