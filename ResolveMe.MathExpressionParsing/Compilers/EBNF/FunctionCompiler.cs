using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class FunctionCompiler : NonTerminal, ICompiler
    {
        public FunctionCompiler(string name)
            : base(name)
        {
        }

        public ICompileResult Compile(string value)
        {
            var structure = ExpressionStructure(value);
            if (structure is null || structure.Any())
            {
                string name = GetName(structure);
                IEnumerable<IToken> arguments = GetArguments(structure);
                return new FunctionToken(name, arguments);
            }
        
            else
                throw new Exception("Compile error");
    }

    private IEnumerable<IToken> GetArguments(IEnumerable<IExpressionItem> structure)
    {
        List<ICompileResult> arguments = new List<ICompileResult>();
        foreach (var item in structure)
        {
            if (item is ICompiler)
            {
                ICompileResult result = ((ICompiler)item).Compile();
                arguments.Add(result);
            }
        }
    }

    private string GetName(IEnumerable<IExpressionItem> structure)
    {
        StringBuilder result = new StringBuilder();
        foreach (var item in structure)
        {
            if (item is NonTerminal)
            {

            }
        }
    }
}
}
