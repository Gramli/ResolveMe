using System.Collections.Generic;
using System.Text;

namespace Parser.ExpressionTokens
{
    public class FunctionToken : IToken
    {
        private readonly string _functionName;

        private readonly List<IToken> _arguments;

        public FunctionToken(string name, Queue<IToken> arguments)
        {
            this._functionName = name;
            this._arguments = new List<IToken>(arguments);
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