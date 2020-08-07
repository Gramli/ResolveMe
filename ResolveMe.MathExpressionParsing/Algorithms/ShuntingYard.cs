using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ShuntingYard
    {
        public PostfixNotation ConvertToPostfix(IEnumerable<IExpressionToken> rawNotation)
        {
            var postfixNotation = Postfix(rawNotation);
            return new PostfixNotation(postfixNotation);
        }

        private IEnumerable<IExpressionToken> Postfix(IEnumerable<IExpressionToken> rawNotation, FunctionToken recursiveFunctionToken = null)
        {
            var output = new List<IExpressionToken>();
            var stack = new Stack<IExpressionToken>();

            var rawNotationArray = rawNotation.ToArray();
            var functionArguments = 1;
            for (var i = 0; i < rawNotationArray.Length; i++)
            {
                var token = rawNotationArray[i];

                switch (token)
                {
                    case SignToken _:
                    case VariableToken _:
                    case NumberToken _:
                        output.Add(token);
                        break;
                    case CommaToken commaToken:
                        functionArguments++;
                        while (stack.TryPop(out var stackOperator))
                        {
                            if (stackOperator is LeftBracketToken)
                            {
                                break;
                            }
                            output.Add(stackOperator);
                        }
                        stack.Push(commaToken);

                        break;
                    case LeftBracketToken leftBracketToken:
                        stack.Push(leftBracketToken);
                        break;
                    case RightBracketToken _:
                        {
                            while (stack.TryPop(out var stackOperator))
                            {
                                if (stackOperator is LeftBracketToken 
                                    || stackOperator is CommaToken)
                                {
                                    break;
                                }
                                output.Add(stackOperator);
                            }
                        }
                        break;
                    case FunctionToken functionToken:
                        {
                            var rangeStart = i + 1;
                            var rangeEnd = i + functionToken.FunctionTokensCount + 1;
                            var functionTokens = rawNotationArray[rangeStart..rangeEnd];
                            i += functionToken.FunctionTokensCount;
                            var result = Postfix(functionTokens, functionToken);
                            output.AddRange(result);
                            output.Add(functionToken);
                        }
                        break;
                    case OperatorToken operatorToken:
                        {
                            while (stack.TryPeek(out var stackOperator))
                            {
                                if (stackOperator is OperatorToken stackOperatorToken &&
                                    (stackOperatorToken.Precedence > operatorToken.Precedence && operatorToken.OperatorAssociativity == OperatorAssociativity.Right ||
                                     stackOperatorToken.Precedence >= operatorToken.Precedence && operatorToken.OperatorAssociativity == OperatorAssociativity.Left))
                                {
                                    stack.Pop();
                                    output.Add(stackOperatorToken);
                                    continue;
                                }
                                break;
                            }
                            stack.Push(operatorToken);
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unknown token: {token.GetStringRepresentation()}");
                }
            }

            if(recursiveFunctionToken != null)
            {
                recursiveFunctionToken.ArgumentsCount = functionArguments;
            }

            if (stack.Count > 0)
            {
                while (stack.TryPop(out var stackToken))
                {
                    output.Add(stackToken);
                }
            }

            return output;
        }
    }
}
