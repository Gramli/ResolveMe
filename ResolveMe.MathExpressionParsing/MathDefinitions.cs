using System;
using System.Collections.Generic;
using System.Text;
using ResolveMe.MathCompiler.ExpressionTokens;

namespace ResolveMe.MathCompiler
{
    public static class MathDefinitions
    {
        public static readonly Dictionary<char, (int, OperatorAssociativity)> OperatorDefinitions = new Dictionary<char, (int, OperatorAssociativity)>()
        {
            { '+',(2, OperatorAssociativity.Left) },
            { '-',(2, OperatorAssociativity.Left) },
            { '*',(3, OperatorAssociativity.Left) },
            { '/',(3, OperatorAssociativity.Left) },
            { '^',(4, OperatorAssociativity.Right) },

        };
    }
}
