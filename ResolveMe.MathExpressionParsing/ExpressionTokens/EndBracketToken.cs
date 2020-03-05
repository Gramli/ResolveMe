namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class EndBracketToken : SpecialCharToken
    {
        public EndBracketToken()
        {

        }
        public EndBracketToken(char bracket) 
            : base(bracket)
        {
        }
    }
}