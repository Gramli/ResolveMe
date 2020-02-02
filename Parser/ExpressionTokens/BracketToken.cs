using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.ExpressionTokens
{
    public class BracketToken : IToken
    {
        private readonly char _bracket;
        public BracketToken(char bracket)
        {
            this._bracket = bracket;
        }
        public object GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
