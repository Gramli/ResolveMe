using Amy;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.Exceptions;
using ResolveMe.MathCompiler.ExpressionTokens;
using System.Collections.Generic;
using System.Linq;
using ResolveMe.MathCompiler.Extensions;
using System;

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

        private IExpressionToken[] Compile(IEnumerable<IExpressionItem> structure)
        {
            var nameAndArguments = GetName(structure);
            var arguments = GetArguments(nameAndArguments.Item2);
            return new IExpressionToken[] { nameAndArguments.Item1,  };
            //TODO DAN
        }

        private IEnumerable<IExpressionToken> GetArguments(IEnumerable<IExpressionItem> structure)
        {
            var result = new List<IExpressionToken>();
            foreach (var expressionItem in structure)
            {
                if (!(expressionItem.Item is ICompiler))
                {
                    ThrowICompileException(expressionItem.Expression);
                }
                var tempResult = ((ICompiler)expressionItem.Item).Compile(expressionItem);
                result.AddRange(tempResult);
            }
            return result;
        }

        private (FunctionNameToken, IList<IExpressionItem>) GetName(IEnumerable<IExpressionItem> structure)
        {
            var name = new FunctionNameToken();
            var index = 0;
            foreach (var expressionItem in structure)
            {
                if (!(expressionItem.Item is ICompiler))
                {
                    ThrowICompileException(expressionItem.Expression);
                }

                if (!(expressionItem.Item is StringCompiler<TextToken>))
                    break;

                var compileResults = ((ICompiler)expressionItem.Item).Compile(expressionItem);
                var textToken = ReadTextToken(compileResults);
                name.Concat(textToken);
                index++;
            }

            if(name.Text.Length < MinFunctionNameLength)
            {
                throw new CompileException("Invalid function name, function name has small number of characters.");
            }

            return (name, structure.Slice(index));
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
