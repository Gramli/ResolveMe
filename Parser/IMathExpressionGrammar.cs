using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    /// <summary>
    /// Interface for matmematical expression grammars
    /// </summary>
    interface IMathExpressionGrammar
    {
        /// <summary>
        /// Non terminal number name
        /// </summary>
        string NumberNonTerminalName { get; }
        /// <summary>
        /// Non terminal variable name
        /// </summary>
        string VariableNonTerminalName { get; }
        /// <summary>
        ///  Non terminal name of infix function
        /// </summary>
        string InfixFuncNonTerminalName { get; }
        /// <summary>
        ///  Non terminal name of prefix function
        /// </summary>
        string PrefixFuncNonTerminalName { get; }
        /// <summary>
        /// Determines that nonTerminal name is result expression if it belongs to grammar
        /// </summary>
        bool IsExpression(string nonTerminalName);
    }
}
