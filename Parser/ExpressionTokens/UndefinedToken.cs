namespace Parser.ExpressionTokens
{
    internal class UndefinedToken : IToken
    {
        public string Value { get; private set; }
        
        public ValueToken(string value)
        {
            this.Value = value;
        }

        public object GetValue()
        {
            throw new System.NotImplementedException();
        }

        public void Concat(UndefinedToken other)
        {
            this.Value = $"{this.Value}{other.Value}";
        }
    }
}