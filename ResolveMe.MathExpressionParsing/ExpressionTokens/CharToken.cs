using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public abstract class CharToken : IToken
    {
        public char Char { get; private set; }

        public CharToken(char value)
        {
            this.Char = value;
        }

        public CharToken()
        {

        }

        public string GetStringRepresentation()
        {
            throw new NotImplementedException();
        }
    }
}
