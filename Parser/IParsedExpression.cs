using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public interface IParsedExpression
    {
        IParsedExpression Parent { get; }
        string EquationString { get; }
        List<IParsedExpression> Childs { get; }
    }
}
