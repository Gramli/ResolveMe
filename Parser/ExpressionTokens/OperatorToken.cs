namespace Parser.ExpressionTokens
{
    public class OperatorToken : IToken
    {
        private readonly string _operatorString;
        
        public OperatorToken(string operatorString)
        {
            this._operatorString = operatorString;
        }

        public string GetStringRepresentation()
        {
            return this._operatorString;
        }
    }
}