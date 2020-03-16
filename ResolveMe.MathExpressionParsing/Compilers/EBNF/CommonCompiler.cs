using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public abstract class CommonCompiler : NonTerminal, ICompiler
    {
        public CommonCompiler(string name, int cacheLength) 
            : base(name, cacheLength)
        {
        }

        protected IEnumerable<IToken> CompileByCycle(string value)
        {
            List<IToken> result = new List<IToken>();
            IEnumerable<IExpressionItem> strItems = ExpressionStructure(value);
            foreach(var item in strItems)
            {
                if (item is ICompiler)
                {
                    IToken itemCompileResult = ((ICompiler)item).Compile(item.Expression);
                    result.Add(itemCompileResult);
                }
                else throw new CompileException($"Item {item} is not Compiler.");
            }
            return result;
        }
        public abstract IToken Compile(string value);
    }
}
