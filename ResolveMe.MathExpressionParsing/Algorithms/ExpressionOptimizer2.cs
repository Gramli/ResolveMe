using Amy.Extensions;
using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Extensions;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ExpressionOptimizer2
    {
        private readonly uint _optimalExpressionLength;
        private readonly uint _optimalArgumentLength = 5;

        public ExpressionOptimizer2(uint optimalExpressionLength)
        {
            this._optimalExpressionLength = optimalExpressionLength;
        }
        public OptimizerResult OptimizeExpression(string expression)
        {
            var cleanExpression = expression.RemoveSpaces().RemoveOuterBrackets(); 

            if (cleanExpression.Length > _optimalExpressionLength)
            {
                if (TryOptimizeByOperatorSplit(cleanExpression, out var result) ||
                    TryOptimizeByFunctionArguments(cleanExpression, out result) ||
                    TryOptimizeByRecursiveFunction(cleanExpression, out result))
                {
                    return result;
                }
            }

            var rawToken = new RawToken(expression);
            return new OptimizerResult(rawToken);
        }

        private bool TryOptimizeByRecursiveFunction(string expression, out OptimizerResult result)
        {
            result = null;
            return false;
        }

        private bool TryOptimizeByFunctionArguments(string expression, out OptimizerResult result)
        {
            result = new OptimizerResult();
            var bracketsCount = 0;
            var lastLeftBracketPosition = -1;
            var lastCommaPosition = -1;

            var editedExpression = expression;

            for (var i = 0; i < expression.Length; i++)
            {
                if (MathDefinitions.BracketsDefinitions.TryGetValue(expression[i], out var left))
                {
                    if (left)
                    {
                        lastLeftBracketPosition = i;
                        bracketsCount++;
                        continue;
                    }

                    bracketsCount--;
                }

                var zeroBrackets = bracketsCount == 0;
                var wasComma = lastCommaPosition != -1;
                if (expression[i] == ',' || zeroBrackets && wasComma)
                {
                    var bracketOrCommaPosition = wasComma ? lastCommaPosition : lastLeftBracketPosition;
                    if (i - bracketOrCommaPosition > _optimalArgumentLength)
                    {
                        var leftArgument = expression[(bracketOrCommaPosition + 1)..i];
                        editedExpression = editedExpression.ReplaceInnerExpressionWithVariable(leftArgument, out var leftArgumentVariable);

                        var leftArgumentTokens = OptimizeExpression(leftArgument);
                        result.AddVariable(leftArgumentVariable, leftArgumentTokens);
                    }

                    lastCommaPosition = zeroBrackets ? -1 : i;
                }
            }

            if (editedExpression == expression)
            {
                result = null;
                return false;
            }

            var expressionTokens = OptimizeExpression(editedExpression);
            result.Add(expressionTokens);
            return true;
        }

        private bool TryOptimizeByOperatorSplit(string expression, out OptimizerResult result)
        {
            var bracketsCount = 0;
            for (var i = 0; i < expression.Length; i++)
            {
                if (MathDefinitions.BracketsDefinitions.TryGetValue(expression[i], out var left))
                {
                    bracketsCount = left ? bracketsCount += 1 : bracketsCount -= 1;
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

        private OperatorToken CreateOperatorToken(char token)
        {
            var (precedence, operatorAssociativity) = MathDefinitions.OperatorDefinitions[token];
            return new OperatorToken(token, precedence, operatorAssociativity);

        }
    }
}
