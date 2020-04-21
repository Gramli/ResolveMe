using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Notations
{
    public class InfixNotation : INotation
    {
        public IEnumerable<IExpressionToken> ExpressionTokens { get; private set; }
        public InfixNotation(IEnumerable<IExpressionToken> tokens)
        {
            this.ExpressionTokens = tokens;
        }
    }
}
