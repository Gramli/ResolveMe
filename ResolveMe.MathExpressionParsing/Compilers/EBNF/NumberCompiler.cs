using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class NumberCompiler : NonTerminal, ICompiler
    {
        public NumberCompiler(string name) 
            : base(name)
        {
        }

        public ICompileResult Compile(string value)
        {
            if (IsExpression(value))
                return new NumberToken(Convert.ToDouble(value));
            else
                throw new Exception("Compile error.");
        }
    }
}
