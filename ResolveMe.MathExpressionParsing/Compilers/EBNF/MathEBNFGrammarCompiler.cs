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
            return ((ICompiler)this.StartSymbol).Compile(value);
        }

    }
}
