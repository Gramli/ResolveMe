using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class CharCompiler<T> : NonTerminal, ICompiler where T : IExpressionToken, new()
    {
        public CharCompiler(string name)
            : base(name)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            if (!IsExpression(value))
            {
                throw new CompileException(value, typeof(CharCompiler<T>));
            }
            return new IExpressionToken[] { (T)Activator.CreateInstance(typeof(T), Convert.ToChar(value)) };
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            if (item is null)
            {
                throw new CompileException("Expression item is null.", typeof(CharCompiler<T>));
            }

            return Compile(item.Expression);
        }
    }
}
