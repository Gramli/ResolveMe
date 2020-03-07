using Amy.EBNF.EBNFItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class ExpressionCompiler : NonTerminal, ICompiler
    {
        public ExpressionCompiler(string name) 
            : base(name)
        {
        }

        public ICompileResult Compile(string value)
        {
            if (IsExpression(value))
            {
                
            }
            else
                throw new Exception("Compile error");
        }
    }
}
