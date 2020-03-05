using Amy;
using Amy.EBNF;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        public MathEBNFGrammarCompiler(IStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public ICompileResult Compile(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
