using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class FunctionToken : IExpressionToken
    {
        public string FunctionName { get; private set; }

        public List<IExpressionToken> Arguments { get; private set; }

        public FunctionToken(string name, IEnumerable<IExpressionToken> arguments)
        {
            this.FunctionName = name;
            this.Arguments = new List<IExpressionToken>(arguments);
        }

        public string GetStringRepresentation()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.FunctionName);
            foreach (var argument in this.Arguments)
            {
                result.Append(argument.GetStringRepresentation());
            }

            return result.ToString();
        }
    }
}