using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class StringCompiler<T> : NonTerminal, ICompiler where T : IToken, new()
    {
        public StringCompiler(string name) 
            : base(name, 20)
        {
        }

        public IToken Compile(string value)
        {
            if (IsExpression(value))
            {
                return (T)Activator.CreateInstance(typeof(T), value);
            }
            else 
                throw new CompileException(value, typeof(StringCompiler<T>));
        }
    }
}
