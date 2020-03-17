using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class StringCompiler<T> : NonTerminal, ICompiler where T : IExpressionToken, new()
    {
        public StringCompiler(string name) 
            : base(name, 20)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            if (IsExpression(value))
            {
                return new IExpressionToken[] { (T)Activator.CreateInstance(typeof(T), value) };
            }
            else 
                throw new CompileException(value, typeof(StringCompiler<T>));
        }
    }
}
