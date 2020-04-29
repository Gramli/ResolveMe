namespace ResolveMe.MathCompiler.ExpressionTokens
{
    //TODO DAN THINK ABOUT SETTING COUNTS
    public class FunctionToken : TextToken
    {
        internal int FunctionTokensCount { get; set; }

        public int ArgumentsCount { get; internal set; }

        internal FunctionToken()
        {
        }
        public FunctionToken(string functionName)
            : base(functionName)
        {
        }
    }
}
