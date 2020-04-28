using System.Globalization;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class NumberToken : IExpressionToken
    {
        public double Value { get; private set; }
        
        public NumberToken(double number)
        {
            this.Value = number;
        }

        public NumberToken(string number)
        {
            this.Value = double.Parse(number, System.Globalization.NumberStyles.Float);
        }

        public string GetStringRepresentation()
        {
            return this.Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}