using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Notations
{
    public class PrefixNotation : INotation
    {
        public IEnumerable<IExpressionToken> ExpressionTokens => throw new NotImplementedException();
    }
}
