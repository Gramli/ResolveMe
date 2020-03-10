using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class ExpressionCompiler : NonTerminal, ICompiler
    {
        public ExpressionCompiler(string name) 
            : base(name, 5)
        {
        }

        public IToken Compile(string value)
        {
            throw new NotImplementedException();
        }
    }
}
