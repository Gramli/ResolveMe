namespace Parser.ExpressionTokens
{
    public class StartBracketToken : SpecialCharToken
    {
        public char EndBracket { get; private set; }
        public StartBracketToken(char bracket, char endBracket) 
            : base(bracket)
        {
            this.EndBracket = endBracket;
        }
    }
}