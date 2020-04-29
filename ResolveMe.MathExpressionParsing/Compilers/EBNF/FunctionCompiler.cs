using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using ResolveMe.MathCompiler.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    internal class FunctionCompiler : NonTerminal, ICompiler
    {
        internal static int MinFunctionNameLength { get; set; }
        public FunctionCompiler(string nonTerminalName)
            : base(nonTerminalName)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            var structure = ExpressionStructure(value).ToList();
            if (structure.IsNullOrEmpty())
            {
                throw new CompileException($"Expression structure is null or do not have any item. Expression: {value}", typeof(FunctionCompiler));
            }

            return Compile(structure);

        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            if (item is null || item.Childs.IsNullOrEmpty())
            {
                throw new CompileException($"Expression item is null or item do not has any childs. Expression: {item.Expression}", typeof(FunctionCompiler));
            }
            return Compile(item.Childs);
        }

        private IList<IExpressionToken> Compile(IEnumerable<IExpressionItem> structure)
        {
            var name = new FunctionToken();
            var result = new List<IExpressionToken>(5) {name};
            var lefBracketReached = false;

            foreach (var expressionItem in structure)
            {
                if (!(expressionItem.Item is ICompiler))
                {
                    ThrowICompileException(expressionItem.Expression);
                }

                if (!lefBracketReached)
                {
                    lefBracketReached = expressionItem.Item is CharCompiler<LeftBracketToken>;
                }

                var compileResults = ((ICompiler)expressionItem.Item).Compile(expressionItem);

                if (lefBracketReached)
                {
                    name.FunctionTokensCount += compileResults.Count();
                    result.AddRange(compileResults);
                    continue;
                }

                if ((expressionItem.Item is StringCompiler<TextToken>))
                {
                    var textToken = ReadTextToken(compileResults);
                    name.Concat(textToken);
                    continue;
                }

                if (name.Text.Length < MinFunctionNameLength)
                {
                    throw new CompileException("Invalid function name, function name has small number of characters.");
                }
            }

            return result;
        }

        private TextToken ReadTextToken(IEnumerable<IExpressionToken> expressionTokens)
        {
            var result = new TextToken();
            foreach (var expressionToken in expressionTokens)
            {
                if (expressionToken is TextToken textToken)
                {
                    result.Concat(textToken);
                }
                break;
            }
            return result;
        }

        private void ThrowICompileException(string expression)
        {
            throw new CompileException(expression, typeof(ICompiler));
        }
    }
}
