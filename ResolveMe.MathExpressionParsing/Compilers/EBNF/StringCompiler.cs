﻿using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class StringCompiler<T> : NonTerminal, ICompiler where T : IExpressionToken, new()
    {
        public StringCompiler(string name)
            : base(name)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            if (!IsExpression(value))
            {
                throw new CompileException(value, typeof(StringCompiler<T>));
            }

            return new IExpressionToken[] { (T)Activator.CreateInstance(typeof(T), value) };
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            if (item is null)
            {
                throw new CompileException("Expression item is null.", typeof(StringCompiler<T>));
            }

            return Compile(item.Expression);
        }
    }
}
