using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expressions.Variables
{
  static class VariableStore
  {
    public static Dictionary<string, string> store { get; private set; } = new Dictionary<string, string>();

    public static void StoreSet(string key, string val)
    {
      store.Add(key, val);
    }

    public static string StoreGet(string key)
    {
      return store[key];
    }
  }
}
