using Amy;

namespace ResolveMe.MathCompiler
{
    public interface IExpressionToken
    {
        /// <summary>
        /// Create token as string
        /// </summary>
        /// <returns></returns>
        string GetStringRepresentation();
    }
}