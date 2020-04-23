namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class FunctionNameToken : TextToken
    {
        internal int FunctionTokensCount { get; set; }

        internal FunctionNameToken()
        {
        }
        public FunctionNameToken(string functionName)
            : base(functionName)
        {
        }
    }
}
