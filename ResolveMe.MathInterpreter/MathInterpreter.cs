using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathInterpreter
{
    internal class MathInterpreter : IInterpreter
    {
        public object Interpret(INotation notation, IContext context)
        {
            object result = null;
            switch (notation)
            {
                case PostfixNotation postfixNotation:
                    result = InterpretPostifix(postfixNotation, context);
                    break;
            }
            return result;
        }

        private object InterpretPostifix(PostfixNotation postfixNotation, IContext context)
        {
            var operandsStack = new Stack<object>();
            foreach (var item in postfixNotation.ExpressionTokens)
            {
                switch (item)
                {
                    case VariableToken varToken:
                        context.TryGetVariableValue(varToken.Text, out object varValue);
                        operandsStack.Push(varValue);
                        break;
                    case NumberToken numberToken:
                        operandsStack.Push(numberToken.Value);
                        break;
                    case OperatorToken operatorToken:
                        InterpretFunction(operandsStack, context, operatorToken.Char.ToString(), 2);
                        break;
                    case FunctionNameToken functionToken:
                        InterpretFunction(operandsStack, context, functionToken.Text, functionToken.FunctionTokensCount);
                        break;
                }
            }
            return operandsStack.Pop();
        }

        private void InterpretFunction(Stack<object> operandsStack, IContext context, string functionName, int argumentsCount)
        {
            var args = new object[argumentsCount];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = operandsStack.Pop();
            }
            context.TryGetFunction(functionName, out var func);
            var result = func.Invoke(args);
            operandsStack.Push(result);
        }
    }
}
