namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class StartBracketToken : SpecialCharToken
    {
        public char EndBracket { get; private set; }

        public StartBracketToken()
        {

        }
        public StartBracketToken(char bracket, char endBracket) 
            : base(bracket)
        {
            this.EndBracket = endBracket;
        }
    }
}