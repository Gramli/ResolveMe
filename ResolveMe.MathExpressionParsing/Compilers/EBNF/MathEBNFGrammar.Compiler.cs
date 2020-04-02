using Amy;
using Amy.Grammars.EBNF;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class MathEBNFGrammarCompiler : EBNFGrammar, ICompiler
    {
        public MathEBNFGrammarCompiler(IStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public IEnumerable<IExpressionToken> Compile(string value)
        {
            //TODO DAN
            return ((ICompiler)this.StartSymbol).Compile(value);
        }

        public IEnumerable<IExpressionToken> Compile(IExpressionItem item)
        {
            //TODO DAN
            throw new System.NotImplementedException();
        }
    }
}
