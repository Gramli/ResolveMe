using Amy;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class OperatorCompiler : CharCompiler<OperatorToken>
    {
        public OperatorCompiler(string name) 
            : base(name)
        {

        }

        public override IEnumerable<IExpressionToken> Compile(string value)
        {
            var token = CreateInstance(value);

            if(!MathDefinitions.OperatorDefinitions.ContainsKey(token.Char))
            {
                throw new CompileException($"Unknown operator: {token.Char}");
            }

            var (precedence, operatorAssociativity) = MathDefinitions.OperatorDefinitions[token.Char];
            token.Precedence = precedence;
            token.OperatorAssociativity = operatorAssociativity;
            return new IExpressionToken[] { token };
        }

        public override IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            return Compile(item.Expression);
        }
    }
}
