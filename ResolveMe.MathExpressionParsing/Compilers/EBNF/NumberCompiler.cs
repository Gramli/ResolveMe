using Amy;
using Amy.Extensions;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class NumberCompiler : NonTerminal, ICompiler
    {
        public NumberCompiler(string name) 
            : base(name)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            if (!IsExpression(value))
            {
                throw new CompileException(value, typeof(NumberCompiler));
            }
            return new IExpressionToken[] { new NumberToken(Convert.ToDouble(value, CultureInfo.InvariantCulture)) };
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
