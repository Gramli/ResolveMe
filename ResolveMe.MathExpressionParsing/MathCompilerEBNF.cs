using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.Compilers.EBNF;
using ResolveMe.MathCompiler.Notations;
using System;

namespace ResolveMe.MathCompiler
{
    public class MathCompilerEBNF : IMathCompiler
    {
        private readonly ICompiler grammarCompiler;
        public MathCompilerEBNF()
        {
            var parser = new EBNFGrammarParserCustom(50);
            var definition = new MathEBNFGrammarDefinition();
            var startSymbol = parser.Parse(definition);
            this.grammarCompiler = new MathEBNFGrammarCompiler(startSymbol);
        }
        public InfixNotation CompileToInfix(string value)
        {
            return new InfixNotation(grammarCompiler.Compile(value));
        }

        public PostfixNotation CompileToPostfix(string value)
        {
            throw new NotImplementedException();
        }

        public PrefixNotation CompileToPrefix(string value)
        {
            throw new NotImplementedException();
        }
    }
}
