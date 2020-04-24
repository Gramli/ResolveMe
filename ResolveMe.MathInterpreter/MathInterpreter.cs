using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.Notations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathInterpreter
{
    public class MathInterpreter : IInterpreter
    {
        public IMathCompiler MathCompiler { get; private set; }

       private double Interpret(IContext context)
        {
            
        }

        public double Evaluate(string expression, IContext context)
        {
            var postfixNotation = MathCompiler.CompileToPostfix(expression);

        }
    }
}
