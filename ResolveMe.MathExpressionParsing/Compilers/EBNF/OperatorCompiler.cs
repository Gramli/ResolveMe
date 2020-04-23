using Amy;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class OperatorCompiler : CharCompiler<OperatorToken>
    {
        private static readonly Dictionary<char, (int, OperatorAssociativity)> _operatorDefinitions = new Dictionary<char, (int, OperatorAssociativity)>()
        {
            { '+',(2, OperatorAssociativity.Left) },
            { '-',(2, OperatorAssociativity.Left) },
            { '*',(3, OperatorAssociativity.Left) },
            { '/',(3, OperatorAssociativity.Left) },
            { '^',(4, OperatorAssociativity.Right) },

        };
        public OperatorCompiler(string name) 
            : base(name)
        {

        }

        public override IEnumerable<IExpressionToken> Compile(string value)
        {
            var token = CreateInstance(value);

            if(!_operatorDefinitions.ContainsKey(token.Char))
            {
                throw new CompileException($"Unknown operator: {token.Char}");
            }

            var tokenProperties = _operatorDefinitions[token.Char];
            token.Precedence = tokenProperties.Item1;
            token.OperatorAssociativity = tokenProperties.Item2;
            return new IExpressionToken[] { token };
        }

        public override IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            return Compile(item.Expression);
        }
    }
}
