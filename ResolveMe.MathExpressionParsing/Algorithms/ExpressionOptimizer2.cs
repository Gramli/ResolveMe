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
            _optimalExpressionLength = optimalExpressionLength;
        }
        public OptimizerResult OptimizeExpression(string expression)
        {
            var cleanExpression = expression.RemoveSpaces().RemoveOuterBrackets();

            if (cleanExpression.Length > _optimalExpressionLength || IsFunctionRecursion(expression))
            {
                if (TryOptimizeByOperatorSplit(cleanExpression, out var result) ||
                    TryOptimizeByArguments(cleanExpression, out result) ||
                    TryOptimizeByRecursiveFunction(cleanExpression, out result))
                {
                    return result;
                }
            }

            var rawToken = new RawToken(expression);
            return new OptimizerResult(rawToken);
        }

        private bool IsFunctionRecursion(string expression)
        {
            var functionBracketes = 0;
            for (var i = 1; i < expression.Length; i++)
            {
                if (expression[i] == MathDefinitions.leftRoundBracket &&
                    !(MathDefinitions.OperatorDefinitions.ContainsKey(expression[i - 1]) || expression[i - 1] == MathDefinitions.argumentSeparator))
                {
                    functionBracketes++;
                }
            }

            return functionBracketes >= 2;
        }

        private bool TryOptimizeByRecursiveFunction(string expression, out OptimizerResult result)
        {
            result = new OptimizerResult();
            var countStarted = false;
            var bracketsCount = 0;
            var leftBracketLocation = -1;
            for (var i = 1; i < expression.Length; i++)
            {
                if (MathDefinitions.BracketsDefinitions.TryGetValue(expression[i], out var left))
                {
                    if (!countStarted)
                    {
                        var charBefore = expression[i - 1];
                        countStarted = left && (charBefore.IsLetter() || charBefore.IsNumber());
                    }

                    if (countStarted)
                    {
                        bracketsCount = left ? bracketsCount += 1 : bracketsCount -= 1;

                        if(leftBracketLocation == -1)
                        {
                            leftBracketLocation = i;
                        }
                    }
                }

                if(countStarted && bracketsCount == 0)
                {
                    var innerFunction = expression[(leftBracketLocation+1)..i];
                    var editedExpression = expression.ReplaceInnerExpressionWithVariable(innerFunction, out var innerFunctionVariable);

                    var expressionTokens = OptimizeExpression(editedExpression);
                    var innerFunctionTokens = OptimizeExpression(innerFunction);

                    result.AddVariable(innerFunctionVariable, innerFunctionTokens);
                    result.Add(expressionTokens);

                    return true;
                }
            }

            result = null;
            return false;
        }

        private bool TryOptimizeByArguments(string expression, out OptimizerResult result)
        {
            result = new OptimizerResult();
            var bracketsCount = 0;
            var lastCommaPosition = -1;

            var editedExpression = expression;

            for (var i = 0; i < expression.Length; i++)
            {
                if (MathDefinitions.BracketsDefinitions.TryGetValue(expression[i], out var left))
                {
                    bracketsCount = left ? bracketsCount += 1 : bracketsCount -= 1;
                }

                var zeroBrackets = bracketsCount == 0;
                var wasComma = lastCommaPosition != -1;
                //its separator or its last argument
                if (expression[i] == MathDefinitions.argumentSeparator || wasComma && zeroBrackets)
                {
                    var tempBracketsCount = 0;
                    var bracketOrCommaPosition = -1;
                    for (var j = i - 1; j >= 0; j--)
                    {
                        if(expression[j] == MathDefinitions.argumentSeparator && tempBracketsCount == 0)
                        {
                            bracketOrCommaPosition = j;
                            break;
                        }

                        if (MathDefinitions.BracketsDefinitions.TryGetValue(expression[j], out var tempLeft))
                        {
                            tempBracketsCount = tempLeft ? tempBracketsCount += 1 : tempBracketsCount -= 1;
                        }

                        if(tempBracketsCount % 2 == 1)
                        {
                            bracketOrCommaPosition = j;
                            break;
                        }
                    }

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
