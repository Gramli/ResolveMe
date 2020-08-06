using System;
using System.Collections.Generic;
using System.Text;
using ResolveMe.MathCompiler.ExpressionTokens;

namespace ResolveMe.MathCompiler
{
    public static class MathDefinitions
    {
        public const char leftRoundBracket = '(';
        public const char rightRoundBracket = ')';
        public const char leftSqareBracket = '[';
        public const char rightSqareBracket = ']';
        public const char leftBrace = '{';
        public const char rightBrace = '}';
        public const char argumentSeparator = ',';

        public static readonly Dictionary<char, (int, OperatorAssociativity)> OperatorDefinitions = new Dictionary<char, (int, OperatorAssociativity)>()
        {
            { '+',(2, OperatorAssociativity.Left) },
            { '-',(2, OperatorAssociativity.Left) },
            { '*',(3, OperatorAssociativity.Left) },
            { '/',(3, OperatorAssociativity.Left) },
            { '^',(4, OperatorAssociativity.Right) },
        };

        public static readonly Dictionary<char, bool> BracketsDefinitions = new Dictionary<char, bool>()
        {
            { leftRoundBracket, true },
            { rightRoundBracket, false },
            { leftSqareBracket, true },
            { rightSqareBracket, false },
            { leftBrace, true },
            { rightBrace, false },
        };
    }
}
