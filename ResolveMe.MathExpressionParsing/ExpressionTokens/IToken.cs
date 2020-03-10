using Amy;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public interface IToken
    {
        /// <summary>
        /// Create token as string
        /// </summary>
        /// <returns></returns>
        string GetStringRepresentation();
    }
}