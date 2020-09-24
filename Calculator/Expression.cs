﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
  public interface Expression
  {
    public string name { get; }
    public string[] shorthands { get; }
    public string helpMessage { get; }

    public object RunExpression(params object[] args);
  }
}
