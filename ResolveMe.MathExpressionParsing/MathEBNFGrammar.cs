using Amy;
using Amy.EBNF;

namespace ResolveMe.MathCompiler
{
    public class MathEBNFGrammar : EBNFGrammar
    {
        public MathEBNFGrammar(IStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public ICompileResult Compile(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
