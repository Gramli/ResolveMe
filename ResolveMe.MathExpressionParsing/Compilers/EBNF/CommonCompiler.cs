using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.Extensions;
using System.Collections.Generic;
using System.Linq;

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
            var result = new List<IExpressionToken>();

            var valueStructure = ExpressionStructure(value);

            if (valueStructure is null || !valueStructure.Any())
            {
                throw new CompileException($"Expression structure is null or do not have any item. Expression: {value}", typeof(CommonCompiler));
            }

            foreach (var item in valueStructure)
            {
                var childItemStructure = Compile(item.Childs);
                result.AddRange(childItemStructure);
            }

            return result;
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            if (item is null || item.Childs.IsNullOrEmpty())
            {
                throw new CompileException($"Expression item is null or item do not has any childs. Expression: {item.Expression}", typeof(CommonCompiler));
            }

            return Compile(item.Childs);
        }

        private IEnumerable<IExpressionToken> Compile(IEnumerable<IExpressionItem> structure)
        {
            var result = new List<IExpressionToken>();
            foreach (var item in structure)
            {
                if (!(item.Item is ICompiler))
                {
                    throw new CompileException($"Item {item} is not Compiler.");
                }

                var itemStructure = ((ICompiler)item.Item).Compile(item);
                result.AddRange(itemStructure);
            }
            return result;
        }
    }
}
