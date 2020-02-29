namespace ResolveMe.MathExpressionParsing.ExpressionTokens
{
    internal class UndefinedToken : IToken
    {
        public string Value { get; private set; }
        
        public UndefinedToken(string value)
        {
            this.Value = value;
        }

        public string GetStringRepresentation()
        {
            return this.Value;
        }

        public void Concat(UndefinedToken other)
        {
            this.Value = $"{this.Value}{other.Value}";
        }
    }
}