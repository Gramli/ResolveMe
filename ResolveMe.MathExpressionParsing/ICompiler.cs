using ResolveMe.MathCompiler.ExpressionTokens;

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
        IToken Compile(string value);
    }
}
