using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public interface IEquation
    {
        IEquation Parent { get; }
        string Equation { get; }
        List<IEquation> Childs { get; }
    }
}
