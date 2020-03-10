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

        public IToken Compile(string value)
        {
            var structure = ExpressionStructure(value).ToList();
            if (structure is null || structure.Any())
            {
                string name = GetName(structure);
                IEnumerable<IToken> arguments = GetArguments(structure);
                return new FunctionToken(name, arguments);
            }
            else
                throw new CompileException(value, typeof(FunctionCompiler));
        }

        private IEnumerable<IToken> GetArguments(IList<IExpressionItem> structure)
        {
            List<IToken> result = new List<IToken>();
            for (int i = 4; i < structure.Count; i++)
            {
                if (structure[i] is ICompiler)
                {
                    IToken tempResult = ((ICompiler)structure[i]).Compile(structure[i].Expression);
                    result.Add(tempResult);
                }
                else
                    ThrowICompileException(structure[i].Expression);
            }
            return result;
        }

        private string GetName(IList<IExpressionItem> structure)
        {
            TextToken result = new TextToken();
            for (int i = 0; i < 3; i++)
            {
                if (structure[i] is ICompiler)
                {
                    IToken compileResult = ((ICompiler)structure[i]).Compile(structure[i].Expression);
                    if (compileResult is TextToken)
                        result.Concat((TextToken)compileResult);
                    else
                        throw new CompileException(structure[i].Expression, typeof(TextToken));
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
