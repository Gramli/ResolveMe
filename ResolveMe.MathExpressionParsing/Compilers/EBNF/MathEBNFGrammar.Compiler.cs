﻿using System;
using Amy;
using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        private readonly ExpressionOptimizer optimizer;

        public MathEBNFGrammarCompiler(IStartSymbol startSymbol)
            : base(startSymbol)
        {
            this.optimizer = new ExpressionOptimizer();
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            var result = new List<IExpressionToken>();
            var rawTokens = optimizer.SplitLongExpression(value);

            foreach (var token in rawTokens)
            {
                if (!(token is RawToken))
                {
                    result.Add(token);
                }
                else
                {
                    var rawTokenString = token.GetStringRepresentation();
                    result.AddRange(CompileStringValue(rawTokenString));
                }
            }

            return result;
        }

        private IEnumerable<IExpressionToken> CompileLongStringValue(string value)
        {
            //if (value.Length > 15 && !string.IsNullOrEmpty(value))
            //{
            //    var editedValue = optimizer.TryRemoveOuterBrackets(value);
            //    var expressions = new List<IExpressionToken>();
            //    var operators = new HashSet<char>() { '*', '/', '+', '-', '^' };
            //    var brackets = 0;

            //    for (int i = 0; i < editedValue.Length; i++)
            //    {
            //        if (editedValue[i].Equals('('))
            //        {
            //            brackets++;
            //        }
            //        else if (editedValue[i].Equals(')'))
            //        {
            //            brackets--;
            //        }

            //        if (operators.Contains(editedValue[i]) && brackets == 0 && i != 0)
            //        {
            //            brackets = 0;
            //            expressions.AddRange(CompileLongStringValue(editedValue[..i]));
            //            expressions.Add(new OperatorToken(editedValue[i]));
            //            expressions.AddRange(CompileLongStringValue(editedValue[(i + 1)..]));
            //            break;

            //        }

            //    }

            //    return expressions;
            //}
            //else
            //{
            //    return CompileStringValue(value);
            //}
            throw  new NotImplementedException();
        }

        private IEnumerable<IExpressionToken> CompileStringValue(string value)
        {
            return ((ICompiler)this.StartSymbol).Compile(value);
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            throw new System.NotImplementedException();
        }
    }
}
