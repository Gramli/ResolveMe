using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ShuntingYard
    {
        public ShuntingYard()
        {

        }

        public PostfixNotation ConvertToPostfix(IEnumerable<IExpressionToken> infixNotation)
        {
            var postfixNotation = Postfix(infixNotation);
            return new PostfixNotation(postfixNotation);
        }

        private IEnumerable<IExpressionToken> Postfix(IEnumerable<IExpressionToken> infixNotation)
        {
            var output = new List<IExpressionToken>();
            var stack = new Stack<IExpressionToken>();

            foreach (var item in infixNotation)
            {
                switch (item)
                {
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
                            IExpressionToken token;
                            while (stack.TryPop(out token))
                            {
                                if (token is LeftBracketToken)
                                {
                                    break;
                                }
                                output.Add(stack.Pop());
                            }
                        }
                        break;
                    case FunctionToken function:
                        {
                            stack.Push(function.GetNameAsToken());
                            var argumentsPostfix = Postfix(function.Arguments);
                            output.AddRange(argumentsPostfix);
                        }
                        break;
                    case OperatorToken operatorToken:
                        {
                            IExpressionToken token;
                            while (stack.TryPeek(out token))
                            {
                                switch (token)
                                {
                                    case FunctionNameToken functionTokenName:
                                        output.Add(stack.Pop());
                                        break;
                                    case OperatorToken stackOperatorToken:
                                        if (stackOperatorToken.Precedence > operatorToken.Precedence ||
                                            (stackOperatorToken.Precedence == operatorToken.Precedence && stackOperatorToken.OperatorAssociativity == OperatorAssociativity.Left))
                                        {
                                            output.Add(stack.Pop());
                                        }
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
