namespace Parser.ExpressionTokens
{
    public interface IToken
    {
        /// <summary>
        /// Returns token value boxed in object
        /// </summary>
        /// <returns></returns>
        object GetValue();
    }
}