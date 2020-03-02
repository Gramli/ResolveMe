using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class NumberCompiler : NonTerminal
    {
        public NumberCompiler(string name) 
            : base(name)
        {
        }

        public ICompileResult Compile(string value)
        {
            if(Is(value))
            return new NumberToken(Convert.ToDouble(value));
        }
    }
}
