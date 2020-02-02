using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.ExpressionTokens
{
    public class VariableToken : IToken
    {
        public string VariableName { get; private set; }

        public VariableToken(string variableName)
        {
            this.VariableName = variableName;
        }
        public object GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
