namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class FunctionNameToken : TextToken
    {
        public int FunctionTokensCount { get; set; }

        internal FunctionNameToken()
        {
        }
        public FunctionNameToken(string functionName)
            : base(functionName)
        {
        }
    }
}
