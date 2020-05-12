using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ExpressionOptimizer
    {
        private const char _leftBracket = '(';
        private const char _rightBracket = ')';
        private const char _leftSquareBracket = '[';
        private const char _rightSquareBracket = ']';
        private readonly uint _optimalExpressionLength;

        public ExpressionOptimizer(uint optimalExpressionLength)
        {
            this._optimalExpressionLength = optimalExpressionLength;
        }

        public OptimizerResult OptimizeExpression(string value)
        {
            //Is necessary to optimize expression
            if (value.Length > this._optimalExpressionLength && !string.IsNullOrEmpty(value) || IsFunctionRecursion(value))
            {
                var result = new OptimizerResult();
                //convert unecessary brackets to tokens and get raw expression
                var editedValue = SeparateOuterBrackets(value);

                foreach (var token in editedValue)
                {
                    if (!(token is RawToken))
                    {
                        result.ExpressionTokens.Add(token);
                    }
                    else
                    {

                        result.Add(OptimizeRawToken((RawToken)token));
                    }
                }

                return result;
            }

            return new OptimizerResult(new RawToken(value));
        }

        private bool IsFunctionRecursion(string value)
        {
            var functionBracketes = 0;
            for (var i = 1; i < value.Length; i++)
            {
                if (value[i].Equals(_leftBracket) &&
                    !(MathDefinitions.OperatorDefinitions.ContainsKey(value[i - 1]) || value[i - 1].Equals(',')))
                    functionBracketes++;
            }

            return functionBracketes >= 4;
        }

        private OptimizerResult OptimizeRawToken(RawToken token)
        {
            var tokenStringValue = token.GetStringRepresentation();

            //var expressions = new List<IExpressionToken>();
            var roundBrackets = 0;
            var squareBrackets = 0;
            var wasRight = false;

            for (var i = 0; i < tokenStringValue.Length; i++)
            {
                switch (tokenStringValue[i])
                {
                    case _leftBracket:
                        roundBrackets++;
                        break;
                    case _rightBracket:
                        roundBrackets--;
                        wasRight = true;
                        break;
                    case _leftSquareBracket:
                        squareBrackets++;
                        break;
                    case _rightSquareBracket:
                        squareBrackets--;
                        break;
                }

                //spliting by operator
                if (MathDefinitions.OperatorDefinitions.ContainsKey(tokenStringValue[i]) && roundBrackets == 0 && i != 0)
                {
                    var result = new OptimizerResult();
                    result.Add(OptimizeExpression(tokenStringValue[..i]));
                    result.ExpressionTokens.Add(CreateOperatorToken(tokenStringValue[i]));
                    result.Add(OptimizeExpression(tokenStringValue[(i + 1)..]));

                    return result;
                }

                //spliting by arguments
                if (tokenStringValue[i].Equals(',') && squareBrackets == 0)
                {
                    var result = new OptimizerResult();
                    var leftArgumentStartIndex = CountBracketsUntil(tokenStringValue, i, -1, 1);
                    var leftArgument = tokenStringValue[(leftArgumentStartIndex + 1)..i];
                    var rightArgumentEndIndex = CountBracketsUntil(tokenStringValue, i, 1, -1);
                    var rightArgument = tokenStringValue[(i + 1)..rightArgumentEndIndex];

                    var editedExpression = ReplaceInnerExpressionWithVariable(tokenStringValue, leftArgument, out var leftArgumentVariable);
                    editedExpression = ReplaceInnerExpressionWithVariable(editedExpression, rightArgument, out var rightArgumentVariable);

                    var expressionTokens = OptimizeExpression(editedExpression);
                    var leftArgumentTokens = OptimizeExpression(leftArgument);
                    var rightArgumentTokens = OptimizeExpression(rightArgument);

                    result.AddVariable(leftArgumentVariable, leftArgumentTokens);
                    result.AddVariable(rightArgumentVariable, rightArgumentTokens);
                    result.Add(expressionTokens);

                    return result;
                }
                //splitting by function
                if (roundBrackets % 2 == 1 && wasRight)
                {
                    var lastChar = tokenStringValue[i - 1];
                    if (!MathDefinitions.OperatorDefinitions.ContainsKey(lastChar) && !lastChar.Equals(','))
                    {
                        var result = new OptimizerResult();

                        var tempRoundBrackets = roundBrackets;
                        var wasLeft = false;
                        var start = 0;
                        for (var j = i - 1; j >= 0; j--)
                        {
                            if (tokenStringValue[j].Equals(_leftBracket))
                            {
                                tempRoundBrackets--;
                                wasLeft = true;
                            }

                            if (tempRoundBrackets % 2 != 1 || !wasLeft) continue;

                            start = j;
                            break;
                        }

                        var argument = tokenStringValue[(start + 1)..(i + 1)];
                        var editedExpression = ReplaceInnerExpressionWithVariable(tokenStringValue, argument, out var argumentVariable);

                        var expressionTokens = OptimizeExpression(editedExpression);
                        var argumentTokens = OptimizeExpression(argument);

                        result.AddVariable(argumentVariable, argumentTokens);
                        result.Add(expressionTokens);

                        return result;
                    }
                }
            }

            return new OptimizerResult(token);

        }

        public IEnumerable<IExpressionToken> SeparateOuterBrackets(string value)
        {
            var result = new List<IExpressionToken>();

            if (value[0].Equals(_leftBracket) && value[^1].Equals(_rightBracket))
            {
                var innerValue = value[1..^1];
                var brackets = 0;

                for (var i = 0; i < innerValue.Length; i++)
                {
                    var equalsRightBracket = innerValue[i].Equals(_rightBracket);

                    if (brackets.Equals(0) && equalsRightBracket)
                    {
                        result.Add(new RawToken(value));
                        return result;
                    }
                    else if (innerValue[i].Equals(_leftBracket))
                    {
                        brackets++;
                    }
                    else if (equalsRightBracket)
                    {
                        brackets--;
                    }
                }

                result.Add(new LeftBracketToken());
                result.Add(new RawToken(innerValue));
                result.Add(new RightBracketToken());
                return result;
            }

            result.Add(new RawToken(value));
            return result;
        }

        private string ReplaceInnerExpressionWithVariable(string expression, string innerExpression, out string variable)
        {
            do
            {
                variable = GenerateVariable(2);

            } while (expression.Contains(variable));

            return expression.Replace(innerExpression, variable);
        }

        private string GenerateVariable(int length)
        {
            var result = string.Empty;

            var charArray = "abcdefghijklmnopqrstuvwxyz";
            var generator = new Random();
            for (var i = 0; i < length; i++)
            {
                var index = generator.Next(0, charArray.Length - 1);
                result += charArray[index];
            }

            return result;
        }

        private int CountBracketsUntil(string tokenStringValue, int actualPosition, int step, int endValue)
        {
            var functionBrackets = 0;
            var result = 0;
            for (var j = actualPosition; j < tokenStringValue.Length; j += step)
            {
                if (tokenStringValue[j].Equals(_leftBracket))
                {
                    functionBrackets++;
                }
                else if (tokenStringValue[j].Equals(_rightBracket))
                {
                    functionBrackets--;
                }

                if (functionBrackets != endValue) continue;

                result = j;
                break;
            }

            return result;
        }

        //private string GetFunctionName(string tokenStringValue, int actualPosition)
        //{
        //    var start = 0;
        //    for (var j = actualPosition; j >= 0; j--)
        //    {
        //        if(!tokenStringValue[j].Equals(_leftBracket)) continue;

        //        start = j;
        //        break;
        //    }

        //    return tokenStringValue[start..^actualPosition];
        //}

        private OperatorToken CreateOperatorToken(char token)
        {
            var (precedence, operatorAssociativity) = MathDefinitions.OperatorDefinitions[token];
            return new OperatorToken(token, precedence, operatorAssociativity);

        }
    }
}
