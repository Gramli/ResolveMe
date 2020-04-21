using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler
{
    public interface INotation
    {
        IEnumerable<IExpressionToken> ExpressionTokens { get; }
    }
}
