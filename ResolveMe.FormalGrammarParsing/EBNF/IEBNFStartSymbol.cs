using ResolveMe.FormalGrammarParsing.EBNF.EBNFItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.FormalGrammarParsing.EBNF
{
    public interface IEBNFStartSymbol
    {
        /// <summary>
        /// Recognize if nonterminal rule can apply on value
        /// </summary>
        bool IsNonTerminal(string nonTerminalName, string value);

        /// <summary>
        /// Determines that value is grammar start symbol expression
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsExpression(string value);

        /// <summary>
        /// Get grammar nonterminal by name
        /// </summary>
        NonTerminal GetNonTerminal(string name);
    }
}
