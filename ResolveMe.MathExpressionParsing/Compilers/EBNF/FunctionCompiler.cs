using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;
using System.Linq;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class FunctionCompiler : NonTerminal, ICompiler
    {
        public FunctionCompiler(string nonTerminalName)
            : base(nonTerminalName, 25)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            var structure = ExpressionStructure(value).ToList();
            if (!(structure is null) || structure.Any())
            {
                var name = GetName(structure);
                var arguments = GetArguments(structure);
                return new IExpressionToken[] { new FunctionToken(name, arguments) };
            }
            else
                throw new CompileException(value, typeof(FunctionCompiler));
        }

        private IEnumerable<IExpressionToken> GetArguments(IList<IExpressionItem> structure)
        {
            var result = new List<IExpressionToken>();
            for (int i = 4; i < structure.Count; i++)
            {
                if (structure[i] is ICompiler)
                {
                    var tempResult = ((ICompiler)structure[i]).Compile(structure[i].Expression);
                    result.AddRange(tempResult);
                }
                else
                    ThrowICompileException(structure[i].Expression);
            }
            return result;
        }

        private string GetName(IList<IExpressionItem> structure)
        {
            var result = new TextToken();
            for (int i = 0; i < 3; i++)
            {
                if (structure[i] is ICompiler)
                {
                    var compileResults = ((ICompiler)structure[i]).Compile(structure[i].Expression);
                    foreach (var compileResult in compileResults)
                    {
                        if (compileResult is TextToken)
                            result.Concat((TextToken)compileResult);
                        else
                            throw new CompileException(structure[i].Expression, typeof(TextToken));
                    }
                }
                else
                    ThrowICompileException(structure[i].Expression);
            }
            return result.Text;
        }

        private void ThrowICompileException(string expression)
        {
            throw new CompileException(expression, typeof(ICompiler));
        }
    }
}
