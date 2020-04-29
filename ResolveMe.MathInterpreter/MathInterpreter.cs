using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Notations;
using System.Collections.Generic;

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
                    case FunctionToken functionToken:
                        InterpretFunction(operandsStack, context, functionToken.Text, functionToken.ArgumentsCount);
                        break;
                }
            }
            return operandsStack.Pop();
        }

        private void InterpretFunction(Stack<object> operandsStack, IContext context, string functionName, int argumentsCount)
        {
            //TODO DAN PUSH TO OPERAND STACK SHOULD BE IN MAIN ALGORITHM
            //TODO DAN CREATE ARGUMENTS CAN BE IN DIFFERENT METHOD
            var args = new object[argumentsCount];
            for (var i = args.Length-1; i >= 0; i--)
            {
                args[i] = operandsStack.Pop();
            }
            context.TryGetFunction(functionName, out var func);
            var result = func.Invoke(args);
            operandsStack.Push(result);
        }
    }
}
