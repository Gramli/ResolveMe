using ResolveMe.MathCompiler.ExpressionTokens;
using System;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ExpressionOptimizer2
    {
        private readonly uint _optimalExpressionLength;

        public ExpressionOptimizer2(uint optimalExpressionLength)
        {
            this._optimalExpressionLength = optimalExpressionLength;
        }
        public OptimizerResult OptimizeExpression(string expression)
        {
            if (expression.Length > _optimalExpressionLength)
            {
                if (TryOptimizeByOperatorSplit(expression, out var result) ||
                    TryOptimizeByFunctionArguments(expression, out result) ||
                    TryOptimizeByRecursiveFunction(expression, out result))
                {
                    return result;
                }
            }

            var rawToken = new RawToken(expression);
            return new OptimizerResult(rawToken);
        }

        private bool TryOptimizeByRecursiveFunction(string expression, out OptimizerResult result)
        {
            var bracketsCount = 0;
            for (var i = 0; i < expression.Length; i++)
            {
                if (MathDefinitions.BracketsDefinitions.TryGetValue(expression[i], out var left))
                {
                    bracketsCount = left ? bracketsCount++ : bracketsCount--;
                    continue;
                }

                var isOperator = MathDefinitions.OperatorDefinitions.ContainsKey(expression[i]);
                if (isOperator && bracketsCount == 0)
                {
                    result = new OptimizerResult();
                    result.Add(OptimizeExpression(expression[..i]));
                    result.ExpressionTokens.Add(CreateOperatorToken(expression[i]));
                    result.Add(OptimizeExpression(expression[(i + 1)..]));

                    return true;
                }
            }

            result = null;
            return false;
        }

        private bool TryOptimizeByFunctionArguments(string expression, out OptimizerResult result)
        {
            throw new NotImplementedException();
        }

        private bool TryOptimizeByOperatorSplit(string expression, out OptimizerResult result)
        {
            for (var i = 0; i < expression.Length; i++)
            {
                if(expression[i] == ',')
                {
                    //je potreba cekovat delku argumentu, nemusi by vzdy potreba je nahrazovat za promene...
                }
            }

            result = null;
            return false;
        }

        private OperatorToken CreateOperatorToken(char token)
        {
            var (precedence, operatorAssociativity) = MathDefinitions.OperatorDefinitions[token];
            return new OperatorToken(token, precedence, operatorAssociativity);

        }
    }
}
