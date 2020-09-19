using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
  public abstract class Command
  {
    public string name;
    public string[] shorthands;
    public string helpMessage;

    public abstract void RunCommand(params string[] args);
  }
}
