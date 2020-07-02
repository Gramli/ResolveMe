using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System.Linq;
using System.Collections.Generic;
using ResolveMe.MathCompiler.Exceptions;
using System;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ShuntingYard
    {
        public PostfixNotation ConvertToPostfix(IEnumerable<IExpressionToken> rawNotation)
        {
            var postfixNotation = Postfix(rawNotation);
            return new PostfixNotation(postfixNotation);
        }

        private IEnumerable<IExpressionToken> Postfix(IEnumerable<IExpressionToken> rawNotation)
        {
            var output = new List<IExpressionToken>();
            var stack = new Stack<IExpressionToken>();

            var rawNotationArray = rawNotation.ToArray();

            for(var i =0;i< rawNotationArray.Length;i++)
            {
                var token = rawNotationArray[i];

                switch(token)
                {
                    case SignToken _:
                    case VariableToken _:
                    case NumberToken _:
                        output.Add(token);
                        break;
                    case CommaToken _: 
                        break;
                    case LeftBracketToken leftBracketToken:
                        stack.Push(leftBracketToken);
                        break;
                    case RightBracketToken _:
                        {
                            while (stack.TryPop(out var stackOperator))
                            {
                                if(stackOperator is LeftBracketToken)
                                {
                                    break;
                                }
                                output.Add(stackOperator);
                            }
                        }
                        break;
                    case FunctionToken functionToken:
                        {
                            var functionTokens = rawNotationArray[(i+1)..(functionToken.FunctionTokensCount+1)];
                            i += functionToken.FunctionTokensCount;
                            var result = Postfix(functionTokens);
                            output.AddRange(result);
                            output.Add(functionToken);
                        }
                        break;
                    case OperatorToken operatorToken:
                        {
                            if (stack.TryPeek(out var stackOperator))
                            {
                                if (stackOperator is OperatorToken stackOperatorToken && stackOperatorToken.Precedence > operatorToken.Precedence)
                                {
                                    stack.Pop();
                                    output.Add(stackOperatorToken);
                                }
                            }
                            stack.Push(operatorToken);
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unknown token: {token.GetStringRepresentation()}");
                }
            }

            if(stack.Count > 0)
            {
                while(stack.TryPop(out var stackToken))
                {
                    output.Add(stackToken);
                }
            }

            return output;
        }
    }
}
