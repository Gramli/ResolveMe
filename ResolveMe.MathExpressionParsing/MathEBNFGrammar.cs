using Amy;
using Amy.EBNF;

namespace ResolveMe.MathExpressionParsing
{
    public class MathEBNFGrammar : EBNFGrammar
    {
        public MathEBNFGrammar(IStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public override ICompileResult Compile(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
