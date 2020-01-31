namespace Parser.ExpressionTokens
{
    public class NumberToken : IToken
    {
        private readonly double _value;
        
        public NumberToken(double number)
        {
            this._value = number;
        }

        public object GetValue()
        {
            return this._value;
        }
    }
}