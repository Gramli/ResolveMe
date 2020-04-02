using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class NumberCompiler : NonTerminal, ICompiler
    {
        public NumberCompiler(string name) 
            : base(name, 50)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            if (!IsExpression(value))
            {
                throw new CompileException(value, typeof(NumberCompiler));
            }
            return new IExpressionToken[] { new NumberToken(Convert.ToDouble(value)) };
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            if (item is null)
            {
                throw new CompileException("Expression item is null.", typeof(NumberCompiler));
            }

            return Compile(item.Expression);
        }
    }
}
