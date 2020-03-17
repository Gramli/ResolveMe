using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler
{
    /// <summary>
    /// Represents Compiler
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Compile data to ICompileResult
        /// </summary>
        IEnumerable<IExpressionToken> Compile(string value);
    }
}
