using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class SignToken : CharToken
    {
        public SignToken()
        {
        }

        public SignToken(char value)
            : base(value)
        {
        }
    }
}
