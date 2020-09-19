using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
  public interface Command
  {
    public string name { get; }
    public string[] shorthands { get; }
    public string helpMessage { get; }

    public abstract void RunCommand(params string[] args);
  }
}
