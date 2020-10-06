using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions.Variables
{
  class Load : Expression
  {
    public string name => "load";
    public string[] shorthands => new string[] { "getvar" };
    public string helpMessage => "Loads a variable from the store.";

    public string RunExpression(params string[] args)
    {
      // This function is so ugly and needs to be refactored.
      string returnString = "";
      foreach (string arg in args)
      {
        returnString += $"{VariableStore.StoreGet(arg)} ";
      }
      return returnString.Trim();
    }
  }
}
