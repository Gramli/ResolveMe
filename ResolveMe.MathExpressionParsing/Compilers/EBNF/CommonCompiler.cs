using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
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
            var structure = ExpressionStructure(value);

            if (structure is null || !structure.Any())
            {
                throw new CompileException($"Expression structure is null or do not have any item. Expression: {value}", typeof(CommonCompiler));
            }

            return Compile(structure);

        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            if (item is null || !item.Childs.Any())
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
                if (!(item is ICompiler))
                {
                    throw new CompileException($"Item {item} is not Compiler.");
                }

                var childItemCompileResult = ((ICompiler)item).Compile(item.Expression);
                result.AddRange(childItemCompileResult);
            }
            return result;
        }
    }
}
