using Amy;
using Amy.Grammars.EBNF;
using ResolveMe.MathCompiler.ExpressionTokens;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        public MathEBNFGrammarCompiler(IStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public IExpressionToken Compile(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
