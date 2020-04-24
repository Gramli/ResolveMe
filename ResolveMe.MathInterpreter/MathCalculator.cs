using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.Notations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathInterpreter
{
    public class MathCalculator
    {
        public IContext Context { get; private set; }

        private readonly IInterpreter interpreter;

        public MathCalculator()
        {
            this.Context = new DefaultContext();
        }

        public MathCalculator(IContext context)
        {
            this.Context = context;
        }

        public T Calculate<T>(string expression)
        {

        }

        public T Calculate<T>(params string[] expressions)
        {

        }

        public PostfixNotation GetPostifixNotation(string expression)
        {

        }

        public InfixNotation GetInfixNotation(string expression)
        {

        }

        public PrefixNotation GetPrefixNotation(string expression)
        {

        }

        public IEnumerable<IExpressionToken> GetRawNotation(string expression)
        {

        }
    }
}
