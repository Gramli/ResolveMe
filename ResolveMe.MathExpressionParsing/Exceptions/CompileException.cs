using System;

namespace ResolveMe.MathCompiler.Exceptions
{
    public class CompileException : Exception
    {
        public CompileException(string message)
            : base($"{message}.")
        {

        }

        public CompileException(string expressionValue, Type compiler)
            : base($"This {expressionValue} is not {compiler}.")
        {

        }
    }
}
