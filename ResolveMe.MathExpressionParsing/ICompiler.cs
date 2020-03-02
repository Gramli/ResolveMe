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
        ICompileResult Compile(string value);
    }
}
