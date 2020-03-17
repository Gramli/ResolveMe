using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class RightBracketToken : CharToken
    {
        public RightBracketToken()
        {
        }

        public RightBracketToken(char value)
            : base(value)
        {
        }
    }
}
