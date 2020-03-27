using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class CommonCompiler : NonTerminal, ICompiler
    {
        public CommonCompiler(string name) 
            : base(name, 20)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            List<IExpressionToken> result = new List<IExpressionToken>();
            IEnumerable<IExpressionItem> strItems = ExpressionStructure(value);
            foreach(var item in strItems)
            {
                if (item is ICompiler)
                {
                    IEnumerable<IExpressionToken> itemCompileResult = ((ICompiler)item).Compile(item.Expression);
                    result.AddRange(itemCompileResult);
                }
                else throw new CompileException($"Item {item} is not Compiler.");
            }
            return result;
        }
    }
}
