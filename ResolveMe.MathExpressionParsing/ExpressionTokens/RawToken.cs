using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    internal class RawToken : IExpressionToken
    {
        private readonly string _value;

        public RawToken(string value)
        {
            this._value = value;
        }

        /// <summary>
        /// Create token as string
        /// </summary>
        /// <returns></returns>
        public string GetStringRepresentation()
        {
            return _value;
        }
    }
}
