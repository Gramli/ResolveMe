using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    interface IMathExpressionGrammar
    {
        string NumberNonTerminalName { get; }
        string VariableNonTerminalName { get; }

        string InfixFuncNonTerminalName { get; }

        string PrefixFuncNonTerminalName { get; }
    }
}
