using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.Notations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathInterpreter
{
    public class MathCalculator : IMathCalculator
    {
        public IContext Context { get; private set; }

        private readonly IMathCompiler mathCompiler;
        private readonly IInterpreter mathInterpreter;

        public MathCalculator()
        {
            this.Context = new DefaultContext();
            this.mathCompiler = new MathCompilerEBNF();
        }

        public MathCalculator(IContext context)
        {
            this.Context = context;
            this.mathCompiler = new MathCompilerEBNF();
        }

        public MathCalculator(IMathCompiler mathCompiler)
        {
            this.mathCompiler = mathCompiler;
            this.Context = new DefaultContext();
        }

        public MathCalculator(IContext context, IMathCompiler mathCompiler)
        {
            this.mathCompiler = mathCompiler;
            this.Context = context;
        }

        public T Calculate<T>(string expression)
        {
            var postfixNotation = GetPostifixNotation(expression);
            return (T)this.mathInterpreter.Interpret(postfixNotation, this.Context);
        }

        public T Calculate<T>(params string[] expressions)
        {

        }

        public PostfixNotation GetPostifixNotation(string expression)
        {
            return this.mathCompiler.CompileToPostfix(expression);
        }

        public InfixNotation GetInfixNotation(string expression)
        {
            return this.mathCompiler.CompileToInfix(expression);
        }

        public PrefixNotation GetPrefixNotation(string expression)
        {
            return this.mathCompiler.CompileToPrefix(expression);
        }

        public IEnumerable<IExpressionToken> GetRawNotation(string expression)
        {
            return this.mathCompiler.GetRawNotation(expression);
        }
    }
}
