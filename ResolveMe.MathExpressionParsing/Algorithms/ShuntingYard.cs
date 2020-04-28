using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System.Linq;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ShuntingYard
    {
        public ShuntingYard()
        {

        }

        public PostfixNotation ConvertToPostfix(IEnumerable<IExpressionToken> rawNotation)
        {
            var postfixNotation = Postfix(rawNotation);
            return new PostfixNotation(postfixNotation);
        }

        private IEnumerable<IExpressionToken> Postfix(IEnumerable<IExpressionToken> rawNotation, IEnumerator<IExpressionToken> enumerator = null, int length = 0)
        {
            var output = new List<IExpressionToken>();
            var stack = new Stack<IExpressionToken>();

            //foreach (var item in infixNotation)
            using var actualEnumerator = enumerator ?? rawNotation.GetEnumerator();
            for (var i = 0; (length == 0 || i < length) && actualEnumerator.MoveNext(); i++)
            {
                var item = actualEnumerator.Current;
                switch (item)
                {
                    case SignToken signToken:
                        output.Add(signToken);
                        break;
                    case VariableToken variable:
                        output.Add(variable);
                        break;
                    case NumberToken number:
                        output.Add(number);
                        break;
                    case LeftBracketToken leftBracket:
                        stack.Push(leftBracket);
                        break;
                    case RightBracketToken rightBracket:
                        {
                            //TODO DAN WRONG, it should check if can pop token
                            while (stack.TryPop(out var token)) //peek
                            {
                                if (token is LeftBracketToken)
                                {
                                    break;
                                }
                                if (token is FunctionNameToken || length != 0)
                                {
                                    output.Add(token);
                                }
                            }
                        }
                        break;
                    case FunctionNameToken functionName:
                        stack.Push(functionName);
                        var arguments = Postfix(rawNotation, actualEnumerator, functionName.FunctionTokensCount);
                        functionName.ArgumentsCount = arguments.Count();
                        output.AddRange(arguments);
                        break;
                    case OperatorToken operatorToken:
                        {
                            var look = true;
                            while (look && stack.TryPop(out var token))
                            {
                                switch (token)
                                {
                                    case FunctionNameToken functionTokenName:
                                        output.Add(functionTokenName);
                                        break;
                                    case OperatorToken stackOperatorToken when stackOperatorToken.Precedence > operatorToken.Precedence ||
                                            (stackOperatorToken.Precedence == operatorToken.Precedence && stackOperatorToken.OperatorAssociativity == OperatorAssociativity.Left):
                                        {
                                            output.Add(stackOperatorToken);
                                        }
                                        break;
                                    default:
                                        look = false;
                                        stack.Push(token);
                                        break;
                                }

                            }
                            stack.Push(operatorToken);
                        }
                        break;
                }
            }

            if (stack.Count > 0)
            {
                output.AddRange(stack.ToArray());
            }

            return output;
        }
    }
}
