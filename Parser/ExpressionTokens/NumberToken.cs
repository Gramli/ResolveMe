using System.Globalization;

namespace Parser.ExpressionTokens
{
    public class NumberToken : IToken
    {
        private readonly double _value;
        
        public NumberToken(double number)
        {
            this._value = number;
        }

        public NumberToken(string number)
        {
            this._value = double.Parse(number, System.Globalization.NumberStyles.Float);
        }

        public string GetStringRepresentation()
        {
            return this._value.ToString(CultureInfo.InvariantCulture);
        }
    }
}