using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class FunctionCompiler : NonTerminal
    {
        public FunctionCompiler(string name) 
            : base(name)
        {
        }

        public ICompileResult Compile(string value)
        {
            throw new NotImplementedException();
        }
    }
}
