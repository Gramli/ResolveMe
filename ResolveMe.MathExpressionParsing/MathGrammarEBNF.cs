using Amy.EBNF;

namespace ResolveMe.MathExpressionParsing
{
    public class MathGrammarEBNF : EBNFGrammar, IMathExpressionGrammar
    {
        public MathGrammarEBNF(IEBNFStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public bool IsNumber(string expression)
        {
            return base.IsNonTerminal("double", expression);
        }

        public bool IsVariable(string expression)
        {
            return base.IsNonTerminal("variable", expression);
        }

        public bool IsPrefixFunction(string expression)
        {
            return base.IsNonTerminal("function", expression);
        }
    }
}
