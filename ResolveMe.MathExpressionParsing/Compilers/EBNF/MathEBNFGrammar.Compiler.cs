using Amy;
using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        private readonly ExpressionOptimizer optimizer;

        public MathEBNFGrammarCompiler(IStartSymbol startSymbol)
            : base(startSymbol)
        {
            this.optimizer = new ExpressionOptimizer(20);
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
