using Amy;
using Amy.Grammars.EBNF;
using Amy.Grammars.EBNF.EBNFItems;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class MathEBNFGrammarStartSymbol : EBNFStartSymbol, ICompiler
    {
        public MathEBNFGrammarStartSymbol(NonTerminal startSymbolNonTerminal, IEnumerable<NonTerminal> productionRules) 
            : base(startSymbolNonTerminal, productionRules)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            return ((ICompiler)this.Rule).Compile(value);
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            return ((ICompiler)this.Rule).Compile(item);
        }
    }
}
