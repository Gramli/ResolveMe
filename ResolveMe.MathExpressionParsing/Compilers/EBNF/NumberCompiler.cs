using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class NumberCompiler : NonTerminal, ICompiler
    {
        public NumberCompiler(string name) 
            : base(name, 50)
        {
        }

        public IToken Compile(string value)
        {
            if (IsExpression(value))
                return new NumberToken(Convert.ToDouble(value));
            else
                throw new CompileException(value, typeof(NumberCompiler));
        }
    }
}
