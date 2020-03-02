using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathExpressionParsing.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathExpressionParsing.Compilers
{
    public class FunctionCompiler : NonTerminal
    {
        public FunctionCompiler(string name) 
            : base(name)
        {
        }

        public override ICompileResult Compile(string value)
        {
            throw new NotImplementedException();
        }
    }
}
