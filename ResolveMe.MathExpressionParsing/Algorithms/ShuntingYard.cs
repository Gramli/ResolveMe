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
                        break;
                }
            }
            throw new NotImplementedException();
        }
    }
}
