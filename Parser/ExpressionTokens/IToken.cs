namespace Parser.ExpressionTokens
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