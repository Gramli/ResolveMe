using Parser.EBNF;

namespace Parser
{
    public class MathGrammarEBNF : EBNFGrammar, IMathExpressionGrammar
    {
        public MathGrammarEBNF(IEBNFStartSymbol startSymbol) 
            : base(startSymbol)
        {
        }

        public bool IsNumber(string expression)
        {
            return base.IsNonTerminal("number", expression);
        }

        public bool IsVariable(string expression)
        {
            return base.IsNonTerminal("variable", expression);
        }

        public bool IsPrefixFunction(string expression)
        {
            return base.IsNonTerminal("prefix_function", expression);
        }
    }
}
