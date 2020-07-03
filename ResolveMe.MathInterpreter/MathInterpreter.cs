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
                        {
                            var arguments = operatorToken.OperatorAssociativity == OperatorAssociativity.Left ?
                                GetArguments(operandsStack, 2) : GetArguments(operandsStack, 1);
                            var result = InterpretFunction(arguments, context, operatorToken.Char.ToString());
                            operandsStack.Push(result);
                        }
                        break;
                    case FunctionToken functionToken:
                        {
                            var arguments = GetArguments(operandsStack, functionToken.ArgumentsCount);
                            var result = InterpretFunction(arguments, context, functionToken.Text);
                            operandsStack.Push(result);
                        }
                        break;
                }
            }
            return operandsStack.Pop();
        }

        private object[] GetArguments(Stack<object> operandsStack, int argumentsCount)
        {
            var args = new object[argumentsCount];
            for (var i = args.Length - 1; i >= 0; i--)
            {
                args[i] = operandsStack.Pop();
            }
            return args;
        }

        private object InterpretFunction(object[] arguments, IContext context, string functionName)
        {
            context.TryGetFunction(functionName, out var func);
            return func.Invoke(arguments);
        }
    }
}
