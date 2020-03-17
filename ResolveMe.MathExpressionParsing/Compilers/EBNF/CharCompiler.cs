using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class CharCompiler<T> : NonTerminal, ICompiler where T : IExpressionToken, new()
    {
        public CharCompiler(string name)
            : base(name, 20)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            if (IsExpression(value))
            {
                return new IExpressionToken[] { (T)Activator.CreateInstance(typeof(T), Convert.ToChar(value)) };
            }
            else
                throw new CompileException(value, typeof(CharCompiler<T>));
        }
    }
}
