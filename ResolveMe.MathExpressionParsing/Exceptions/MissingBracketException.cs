using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Exceptions
{
    public class MissingBracketException : ArgumentException
    {
        public MissingBracketException(string message)
            : base(message)
        {

        }
    }
}
