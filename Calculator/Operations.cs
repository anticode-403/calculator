using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
  public static class Operations
  {
    public enum OperableEnum
    {
      Solved,
      Add,
      Subtract,
      Multiply,
      Divide
    }

    public static OperableEnum ParseOperable(char operable)
    {
      OperableEnum value = OperableEnum.Solved;
      switch(operable)
      {
        case '+':
          value = OperableEnum.Add;
          break;
        case '-':
          value = OperableEnum.Subtract;
          break;
        case '*':
          value = OperableEnum.Multiply;
          break;
        case '/':
          value = OperableEnum.Divide;
          break;
      }
      return value;
    }

    public static double SolveOperation(OperableEnum operable, params string[] numsString)
    {
      double[] nums = new double[numsString.Length - 1];
      double result = 0f;
      for (int i = 0; i < numsString.Length; i++)
      {
        if (i == 0)
        {
          result = nums[0];
          continue;
        }
        while (numsString[i - 1].StartsWith('_')) numsString[i - 1].Substring(1);
        nums[i] = double.Parse(numsString[i - 1]);
      }
      switch (operable)
      {
        case OperableEnum.Add:
          foreach (double num in nums) result += num;
          break;
        case OperableEnum.Subtract:
          foreach (double num in nums) result -= num;
          break;
        case OperableEnum.Multiply:
          foreach (double num in nums) result *= num;
          break;
        case OperableEnum.Divide:
          if (nums.Length != 1) throw new Exception("You cannot divide more than 2 things at once.");
          result /= nums[0];
          break;
        case OperableEnum.Solved:
          break;
        default:
          throw new Exception("Operations.SolveOperation was given an operable it cannot yet handle.");
      }
      return result;
    }

    public static double SolveTreeNode(TreeNode treeNode)
    {
      string[] children = new string[treeNode.children.Keys.Count];
      int i = 0;
      foreach (string child in treeNode.children.Keys)
      {
        if (treeNode.GetChild(child).operation != OperableEnum.Solved)
        {
          children[i] = SolveTreeNode(treeNode.GetChild(child)).ToString();
        }
        else children[i] = child;
        i++;
      }

      return SolveOperation(treeNode.operation, children);
    }
  }
}
