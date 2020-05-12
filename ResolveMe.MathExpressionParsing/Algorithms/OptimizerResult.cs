using System;
using System.Collections.Generic;
using System.Text;
using ResolveMe.MathCompiler.ExpressionTokens;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class OptimizerResult
    {
        public List<IExpressionToken> ExpressionTokens { get; }

        public Dictionary<string, OptimizerResult> VariableTokens { get; }

        public OptimizerResult()
        {
            this.ExpressionTokens = new List<IExpressionToken>(20);
            this.VariableTokens = new Dictionary<string, OptimizerResult>(2);
        }

        public OptimizerResult(IExpressionToken token)
        {
            this.ExpressionTokens = new List<IExpressionToken>() { token };
            this.VariableTokens = new Dictionary<string, OptimizerResult>(0);
        }

        public void Add(OptimizerResult resultToAdd)
        {
            foreach (var variableToken in resultToAdd.VariableTokens)
            {
                AddVariable(variableToken.Key, variableToken.Value);
            }

            ExpressionTokens.AddRange(resultToAdd.ExpressionTokens);
        }

        public void AddVariable(string name, OptimizerResult variableResult)
        {
            VariableTokens.Add(name, variableResult);
        }
    }
}
