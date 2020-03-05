using Amy.EBNF.EBNFItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class StringCompiler<T> : NonTerminal, ICompiler where T : ICompileResult, new()
    {
        public StringCompiler(string name) 
            : base(name)
        {
        }

        public ICompileResult Compile(string value)
        {
            if (IsExpression(value))
            {
                return (T)Activator.CreateInstance(typeof(T), value);
            }
            else throw new Exception("Compile error");
        }
    }
}
