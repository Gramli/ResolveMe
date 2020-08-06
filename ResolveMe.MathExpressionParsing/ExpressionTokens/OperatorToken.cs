namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class OperatorToken : CharToken
    {
        public int Precedence { get; set; }

        public OperatorAssociativity OperatorAssociativity { get; set; }

        public OperatorToken()
        {

        }

        public OperatorToken(char value)
            : base(value)
        {

        }

        public OperatorToken(char value, int precedence, OperatorAssociativity operatorAssociativity)
            : base(value)
        {
            this.Precedence = precedence;
            this.OperatorAssociativity = operatorAssociativity;
        }
    }
}