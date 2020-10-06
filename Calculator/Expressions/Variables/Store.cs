using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Expressions.Variables
{
  class Store : Expression
  {
    public string name => "store";
    public string[] shorthands => new string[] { "storevar" };
    public string helpMessage => "Creates or updates a variable and stores it.";

    public string RunExpression(params string[] args)
    {
      // Refactor this.
      VariableStore.StoreSet(args[0], string.Join(' ', args.Skip(1).ToArray()));
      return "";
    }
  }
}
