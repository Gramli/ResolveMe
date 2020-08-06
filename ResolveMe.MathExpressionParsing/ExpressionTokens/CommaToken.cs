namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class CommaToken : CharToken
    {
        public CommaToken()
            : base(',')
        {
        }

        public CommaToken(char value)
            : base(value)
        {
        }
    }
}