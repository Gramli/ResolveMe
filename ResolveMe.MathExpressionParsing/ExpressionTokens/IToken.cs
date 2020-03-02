using Amy;

namespace ResolveMe.MathExpressionParsing.ExpressionTokens
{
    public interface IToken : ICompileResult
    {
        /// <summary>
        /// Create token as string
        /// </summary>
        /// <returns></returns>
        string GetStringRepresentation();
    }
}