namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class OperatorToken : CharToken
    {
        //TODO DAN SET PRECEDENCE
        public OperatorToken()
        {
        }

        public OperatorToken(char value) 
            : base(value)
        {
        }
    }
}