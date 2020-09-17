using System;

namespace Calculator
{
  class Calculator
  {
    static void Main()
    {
      Console.WriteLine("Calculator awaiting input...");
      string input = Console.ReadLine();
      TreeNode equation = null;
      try
      {
        equation = TreeNode.BuildTree(input);
      } catch(Exception error)
      {
        Console.WriteLine(error.Message);
      }
      Console.WriteLine(Operations.SolveTreeNode(equation));
      Main();
    }
  }
}
