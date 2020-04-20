using Amy;
using System;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler
{
    /// <summary>
    /// Represents Compiler
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Compile data to collection of expressionTokens
        /// </summary>
        IEnumerable<IExpressionToken> Compile(string value);
        /// <summary>
        /// Compile data to ICompileResult
        /// </summary>
        IEnumerable<IExpressionToken> Compile(IExpressionItem item);
    }
}
