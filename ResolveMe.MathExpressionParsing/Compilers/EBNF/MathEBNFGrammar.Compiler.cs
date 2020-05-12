using Amy;
using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.Algorithms;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        private readonly ExpressionOptimizer optimizer;

        public MathEBNFGrammarCompiler(IStartSymbol startSymbol)
            : base(startSymbol)
        {
            this.optimizer = new ExpressionOptimizer(20);
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            var optimizeResult = optimizer.OptimizeExpression(value);
            return Compile(optimizeResult);
        }

        private IEnumerable<IExpressionToken> Compile(OptimizerResult optimizeResult)
        {
            var result = new List<IExpressionToken>();
            foreach (var token in optimizeResult.ExpressionTokens)
            {
                if (!(token is RawToken))
                {
                    result.Add(token);
                }
                else
                {
                    var rawTokenString = token.GetStringRepresentation();

                    foreach (var compiledToken in CompileStringValue(rawTokenString))
                    {
                        if (compiledToken is VariableToken variableToken && optimizeResult.VariableTokens.ContainsKey(variableToken.Text))
                        {
                            var variableOptimizer = optimizeResult.VariableTokens[variableToken.Text];
                            var variableOptimizerResult = Compile(variableOptimizer);
                            result.AddRange(variableOptimizerResult);
                        }
                        else
                        {
                            result.Add(compiledToken);
                        }
                    }

                }
            }

            return result;
        }

        private List<IExpressionToken> ReplaceVariableTokenWithExpressionTokens(IEnumerable<IExpressionToken> expressionTokens, string variableName, IEnumerable<IExpressionToken> variableTokens)
        {
            var result = new List<IExpressionToken>();

            foreach (var token in expressionTokens)
            {
                if (token is VariableToken variableToken && variableToken.Text.Equals(variableName))
                {
                    result.AddRange(variableTokens);
                }
                else
                {
                    result.Add(token);
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
