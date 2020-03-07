using Amy;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
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
            if (IsExpression(value))
            {
                var structure = FetchLastExpressionStructure();
                string name = GetName(structure);
                IEnumerable<IToken> arguments = GetArguments(structure);
                return new FunctionToken(name, arguments);
            }
            else
                throw new Exception("Compile error");
        }

        private IEnumerable<IToken> GetArguments(IEnumerable<IFormalGrammarItem> structure)
        {
            List<ICompileResult> arguments = new List<ICompileResult>();
            foreach(var item in structure)
            {
                if (item is ICompiler)
                {
                    ICompileResult result = ((ICompiler)item).Compile();
                    arguments.Add(result);
                }
            }
        }

        private string GetName(IEnumerable<IFormalGrammarItem> structure)
        {
            StringBuilder result = new StringBuilder();
            foreach(var item in structure)
            {
                if(item is NonTerminal)
                {

                }
            }
        }
    }
}
