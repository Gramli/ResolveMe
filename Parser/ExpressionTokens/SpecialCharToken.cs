using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.ExpressionTokens
{
    public abstract class SpecialCharToken : IToken
    {
        private readonly char _specialChar;

        protected SpecialCharToken(char bracket)
        {
            this._specialChar = bracket;
        }

        public string GetStringRepresentation()
        {
            return this._specialChar.ToString();
        }
    }
}
