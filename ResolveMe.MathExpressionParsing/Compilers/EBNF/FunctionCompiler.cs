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
            var result = structure.SplitToArray(2, 3); // TODO DAN FIX!!
            var name = GetName(result[0]);
            var arguments = GetArguments(result[1]);
            return new IExpressionToken[] { new FunctionToken(name, arguments) };
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

        private string GetName(IEnumerable<IExpressionItem> structure)
        {
            var result = new TextToken();
            foreach (var expressionItem in structure)
            {
                if (!(expressionItem.Item is ICompiler))
                {
                    ThrowICompileException(expressionItem.Expression);
                }

                var compileResults = ((ICompiler)expressionItem.Item).Compile(expressionItem);
                foreach (var compileResult in compileResults)
                {
                    if (!(compileResult is TextToken))
                    {
                        throw new CompileException(expressionItem.Expression, typeof(TextToken));
                    }
                    result.Concat((TextToken)compileResult);
                }
            }
            return result.Text;
        }

        private void ThrowICompileException(string expression)
        {
            throw new CompileException(expression, typeof(ICompiler));
        }
    }
}
