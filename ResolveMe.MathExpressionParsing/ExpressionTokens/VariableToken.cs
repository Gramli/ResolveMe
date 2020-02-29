using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathExpressionParsing.ExpressionTokens
{
    public class VariableToken : IToken
    {
        public string VariableName { get; private set; }

        public VariableToken(string variableName)
        {
            this.VariableName = variableName;
        }

        public string GetStringRepresentation()
        {
            return this.VariableName;
        }
    }
}
