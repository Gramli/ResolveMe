using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathExpressionParsing.ExpressionTokens;
using System;

namespace ResolveMe.MathExpressionParsing.Compilers
{
    public class NumberCompiler : NonTerminal
    {
        public NumberCompiler(string name) 
            : base(name)
        {
        }

        public override ICompileResult Compile(string value)
        {
            return new NumberToken(Convert.ToDouble(value));
        }
    }
}
