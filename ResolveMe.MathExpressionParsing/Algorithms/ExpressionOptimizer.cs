using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using ResolveMe.MathCompiler.ExpressionTokens;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ExpressionOptimizer
    {
        private const char _leftBracket = '(';
        private const char _rightBracket = ')';
        private const char _leftSquareBracket = '[';
        private const char _rightSquareBracket = ']';
        private readonly uint _optimalExpressionLength;

        private readonly Dictionary<string, List<IExpressionToken>> variableTokens;

        public ExpressionOptimizer(uint optimalExpressionLength)
        {
            this._optimalExpressionLength = optimalExpressionLength;
        }

        public IEnumerable<IExpressionToken> SplitLongExpression(string value)
        {
            if (value.Length > this._optimalExpressionLength && !string.IsNullOrEmpty(value) || IsFunctionRecursion(value))
            {
                var editedValue = TryRemoveOuterBrackets(value);
                var expressions = new List<IExpressionToken>();

                foreach (var token in editedValue)
                {
                    if (!(token is RawToken))
                    {
                        expressions.Add(token);
                    }
                    else
                    {
                        expressions.AddRange(GetTokenTokens(token));
                    }
                }

                return expressions;
            }

            return new List<IExpressionToken>(1) { new RawToken(value) };
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

        private IEnumerable<IExpressionToken> GetTokenTokens(IExpressionToken token)
        {
            var tokenStringValue = token.GetStringRepresentation();

            var expressions = new List<IExpressionToken>();
            var roundBrackets = 0;
            var squareBrackets = 0;

            for (var i = 0; i < tokenStringValue.Length; i++)
            {
                switch (tokenStringValue[i])
                {
                    case _leftBracket:
                        roundBrackets++;
                        break;
                    case _rightBracket:
                        roundBrackets--;
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
                    expressions.AddRange(SplitLongExpression(tokenStringValue[..i]));
                    expressions.Add(CreateOperatorToken(tokenStringValue[i]));
                    expressions.AddRange(SplitLongExpression(tokenStringValue[(i + 1)..]));
                    break;
                }

                //spliting by arguments
                if (tokenStringValue[i].Equals(',') && squareBrackets == 0)
                {
                    var leftArgumentStartIndex = CountBracketsUntil(tokenStringValue, i, -1, 1);
                    var leftArgument = tokenStringValue[leftArgumentStartIndex..^i];
                    var rightArgumentEndIndex = CountBracketsUntil(tokenStringValue, i, 1, -1);
                    var rightArgument = tokenStringValue[i..^rightArgumentEndIndex];
                    var functionName = GetFunctionName(tokenStringValue, i);

                    expressions.Add(new FunctionToken(functionName));
                    expressions.Add(new LeftBracketToken());
                    expressions.AddRange(SplitLongExpression(leftArgument));
                    expressions.Add(new CommaToken());
                    expressions.AddRange(SplitLongExpression(rightArgument));
                    expressions.Add(new RightBracketToken());


                    //create new string with replacement
                }
                //splitting by function
                if (roundBrackets % 2 == 1)
                {
                    var lastChar = tokenStringValue[i - 1];
                    if (!MathDefinitions.OperatorDefinitions.ContainsKey(lastChar) && !lastChar.Equals(','))
                    {
                        //read to brackets = 0
                        //go left for name, end is left bracket or 0
                        //go right end is right bracket and brackets %2 == 0
                    }
                }
            }

            if (expressions.Count == 0)
            {
                expressions.Add(token);
            }

            return expressions;

        }

        public IEnumerable<IExpressionToken> TryRemoveOuterBrackets(string value)
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

        private string GetFunctionName(string tokenStringValue, int actualPosition)
        {
            var start = 0;
            for (var j = actualPosition; j >= 0; j--)
            {
                if(!tokenStringValue[j].Equals(_leftBracket)) continue;

                start = j;
                break;
            }

            return tokenStringValue[start..^actualPosition];
        }

        private OperatorToken CreateOperatorToken(char token)
        {
            var (precedence, operatorAssociativity) = MathDefinitions.OperatorDefinitions[token];
            return new OperatorToken(token, precedence, operatorAssociativity);

        }
    }
}
