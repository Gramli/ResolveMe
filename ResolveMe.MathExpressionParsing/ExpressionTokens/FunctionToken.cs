using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class FunctionToken : IExpressionToken
    {
        private readonly string _functionName;

        private readonly List<IExpressionToken> _arguments;

        public FunctionToken(string name, IEnumerable<IExpressionToken> arguments)
        {
            this._functionName = name;
            this._arguments = new List<IExpressionToken>(arguments);
        }

        public string GetStringRepresentation()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this._functionName);
            foreach (var argument in this._arguments)
            {
                result.Append(argument.GetStringRepresentation());
            }

            return result.ToString();
        }
    }
}