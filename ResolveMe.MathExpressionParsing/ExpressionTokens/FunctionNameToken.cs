namespace ResolveMe.MathCompiler.ExpressionTokens
{
    //TODO DAN RENAME to FunctionToken
    //TODO DAN THINK ABOUT SETTING COUNTS
    public class FunctionNameToken : TextToken
    {
        internal int FunctionTokensCount { get; set; }

        public int ArgumentsCount { get; internal set; }

        internal FunctionNameToken()
        {
        }
        public FunctionNameToken(string functionName)
            : base(functionName)
        {
        }
    }
}
