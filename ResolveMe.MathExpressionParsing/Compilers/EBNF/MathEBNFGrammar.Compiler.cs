using Amy;
using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.Algorithms;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;
using System.Linq;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        private readonly ExpressionOptimizer2 optimizer;

        public MathEBNFGrammarCompiler(IStartSymbol startSymbol)
            : base(startSymbol)
        {
            this.optimizer = new ExpressionOptimizer2(15);
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
                    var functionTokens = new List<FunctionToken>();

                    foreach (var compiledToken in CompileStringValue(rawTokenString))
                    {
                        if (compiledToken is VariableToken variableToken && optimizeResult.VariableTokens.ContainsKey(variableToken.Text))
                        {
                            var variableOptimizer = optimizeResult.VariableTokens[variableToken.Text];
                            var variableOptimizerResult = Compile(variableOptimizer);
                            result.AddRange(variableOptimizerResult);
                            //i need to set new function tokens count
                            foreach (var item in functionTokens)
                            {
                                item.FunctionTokensCount += variableOptimizerResult.Count() - 1;
                            }
                            continue;
                        }
                        else if (compiledToken is FunctionToken functionToken)
                        {
                            functionTokens.Add(functionToken);
                        }

                        result.Add(compiledToken);

                    }

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
