using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.Compilers.EBNF;
using ResolveMe.MathCompiler.Notations;
using System;
using ResolveMe.MathCompiler.Algorithms;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler
{
    public class MathCompilerEBNF : IMathCompiler
    {
        private readonly ICompiler grammarCompiler;
        private readonly ShuntingYard shuntingYard;

        public MathCompilerEBNF()
        {
            var parser = new EBNFGrammarParserCustom(50);
            var definition = new MathEBNFGrammarDefinition();
            var startSymbol = parser.Parse(definition);
            this.grammarCompiler = new MathEBNFGrammarCompiler(startSymbol);
            shuntingYard = new ShuntingYard();
        }
        public InfixNotation CompileToInfix(string expression)
        {
            throw  new NotImplementedException();
        }

        public PostfixNotation CompileToPostfix(string expression)
        {
            var rawNotation = grammarCompiler.Compile(expression);
            return shuntingYard.ConvertToPostfix(rawNotation);
        }

        public PrefixNotation CompileToPrefix(string expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IExpressionToken> GetRawNotation(string expression)
        {
            return grammarCompiler.Compile(expression);
        }
    }
}
