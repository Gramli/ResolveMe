using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class CharCompiler<T> : NonTerminal, ICompiler where T : IToken, new()
    {
        public CharCompiler(string name)
            : base(name, 20)
        {
        }

        public IToken Compile(string value)
        {
            if (IsExpression(value))
            {
                return (T)Activator.CreateInstance(typeof(T), Convert.ToChar(value));
            }
            else
                throw new CompileException(value, typeof(CharCompiler<T>));
        }
    }
}
