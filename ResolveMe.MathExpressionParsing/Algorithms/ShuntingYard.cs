using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System.Linq;
using System.Collections.Generic;
using ResolveMe.MathCompiler.Exceptions;

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

        //private IEnumerable<IExpressionToken> PostfixNew(IEnumerable<IExpressionToken> rawNotation)
        //{
        //    var output = new List<IExpressionToken>();
        //    var stack = new Stack<IExpressionToken>();

        //    foreach (var item in rawNotation)
        //    {
        //        switch (item)
        //        {
        //            case SignToken signToken:
        //                output.Add(signToken);
        //                break;
        //            case VariableToken variable:
        //                output.Add(variable);
        //                break;
        //            case NumberToken number:
        //                output.Add(number);
        //                break;
        //            case FunctionNameToken functionToken:
        //                stack.Push(functionToken);
        //                break;
        //            case LeftBracketToken leftBracket:
        //                stack.Push(leftBracket);
        //                break;
        //            case CommaToken commaToken:

        //                break;
        //            case RightBracketToken rightBracket:
        //                {
        //                    var findLeft = false;
        //                    while (stack.TryPop(out var token))
        //                    {
        //                        if (token is LeftBracketToken)
        //                        {
        //                            findLeft = true;
        //                            break;
        //                        }

        //                        output.Add(token);
        //                    }

        //                    if (!findLeft)
        //                    {
        //                        throw new MissingBracketException("Missing LeftBracketToken in stack.");
        //                    }
        //                }
        //                break;
        //            case OperatorToken operatorToken:
        //            {
        //                var canContinue = true;
        //                    while (canContinue && stack.TryPeek(out var stackToken))
        //                    {
        //                        switch (stackToken)
        //                        {
        //                            case OperatorToken stackOperatorToken:
        //                                if ((operatorToken.OperatorAssociativity == OperatorAssociativity.Left
        //                                     && operatorToken.Precedence <= stackOperatorToken.Precedence) ||
        //                                    (operatorToken.OperatorAssociativity == OperatorAssociativity.Right &&
        //                                     operatorToken.Precedence < stackOperatorToken.Precedence))
        //                                {
        //                                    stack.Pop();
        //                                    output.Add(stackOperatorToken);
        //                                }
        //                                else
        //                                {
        //                                    canContinue = false;
        //                                } 
        //                                break;
        //                        }
        //                    }
        //                    stack.Push(operatorToken);
        //                }
        //                break;
        //        }
        //    }
        //    if (stack.Count > 0)
        //    {
        //        output.AddRange(stack.ToArray());
        //    }

        //    return output;
        //}

        private IEnumerable<IExpressionToken> Postfix(IEnumerable<IExpressionToken> rawNotation)//, IEnumerator<IExpressionToken> enumerator = null, int length = 0)
        {
            var output = new List<IExpressionToken>();
            var stack = new Stack<IExpressionToken>();

            using var actualEnumerator = rawNotation.GetEnumerator();
            for (var i = 0; /*(length == 0 || i < length) &&*/ actualEnumerator.MoveNext(); i++)
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
                            while (stack.TryPop(out var token)) //peek
                            {
                                if (token is LeftBracketToken)
                                {
                                    break;
                                }
                                if (token is FunctionToken)// || length != 0)
                                {
                                    output.Add(token);
                                }
                            }
                        }
                        break;
                    case FunctionToken functionName:
                        stack.Push(functionName);
                        var arguments = PostfixFunction(rawNotation, functionName, actualEnumerator);
                        //functionName.ArgumentsCount = arguments.Count(); //TODO DAN this is stupid, count arguments by Comma ...
                        output.AddRange(arguments);
                        break;
                    case OperatorToken operatorToken:
                        {
                            var look = true;
                            while (look && stack.TryPeek(out var token))
                            {
                                switch (token)
                                {
                                    case FunctionToken functionTokenName:
                                        stack.Pop();
                                        output.Add(functionTokenName);
                                        break;
                                    case OperatorToken stackOperatorToken when
                                        (operatorToken.OperatorAssociativity == OperatorAssociativity.Left
                                         && operatorToken.Precedence <= stackOperatorToken.Precedence) ||
                                        (operatorToken.OperatorAssociativity == OperatorAssociativity.Right &&
                                         operatorToken.Precedence < stackOperatorToken.Precedence):
                                        {
                                            stack.Pop();
                                            output.Add(stackOperatorToken);
                                        }
                                        break;
                                    default:
                                        look = false;
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

        private IEnumerable<IExpressionToken> PostfixFunction(IEnumerable<IExpressionToken> rawNotation, FunctionToken baseFunctionToken, IEnumerator<IExpressionToken> enumerator = null)
        {
            var output = new List<IExpressionToken>();
            var stack = new Stack<IExpressionToken>();

            using var actualEnumerator = enumerator ?? rawNotation.GetEnumerator();
            for (var i = 0; (baseFunctionToken.FunctionTokensCount == 0 || i < baseFunctionToken.FunctionTokensCount) && actualEnumerator.MoveNext(); i++)
            {
                var item = actualEnumerator.Current;
                switch (item)
                {
                    case CommaToken commaToken:
                        baseFunctionToken.ArgumentsCount += 1;
                        break;
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
                            while (stack.TryPop(out var token)) //peek
                            {
                                if (token is LeftBracketToken)
                                {
                                    break;
                                }
                                //if (token is FunctionToken || l)
                                //{
                                    output.Add(token);
                                //}
                            }
                        }
                        break;
                    case FunctionToken functionName:
                        stack.Push(functionName);
                        var arguments = PostfixFunction(rawNotation, functionName, actualEnumerator);
                        //functionName.ArgumentsCount = arguments.Count(); //TODO DAN this is stupid, count arguments by Comma ...
                        output.AddRange(arguments);
                        break;
                    case OperatorToken operatorToken:
                        {
                            var look = true;
                            while (look && stack.TryPeek(out var token))
                            {
                                switch (token)
                                {
                                    case FunctionToken functionTokenName:
                                        stack.Pop();
                                        output.Add(functionTokenName);
                                        break;
                                    case OperatorToken stackOperatorToken when
                                        (operatorToken.OperatorAssociativity == OperatorAssociativity.Left
                                         && operatorToken.Precedence <= stackOperatorToken.Precedence) ||
                                        (operatorToken.OperatorAssociativity == OperatorAssociativity.Right &&
                                         operatorToken.Precedence < stackOperatorToken.Precedence):
                                        {
                                            stack.Pop();
                                            output.Add(stackOperatorToken);
                                        }
                                        break;
                                    default:
                                        look = false;
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
