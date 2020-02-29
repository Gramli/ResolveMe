using ResolveMe.FormalGrammarParsing.EBNF.EBNFItems;

namespace ResolveMe.FormalGrammarParsing.EBNF
{
    /// <summary>
    /// Represents EBNF grammar
    /// </summary>
    public abstract class EBNFGrammar : IEBNFGrammar
    {
        protected readonly IEBNFStartSymbol _startSymbol;
        /// <summary>
        /// Inicialize StartSymbol
        /// </summary>
        /// <param name="name">left side of dedication</param>
        /// <param name="startSymbolRule">rule</param>
        /// <param name="productionRules">production rules in grammar</param>
        public EBNFGrammar(IEBNFStartSymbol startSymbol)
        {
            this._startSymbol = startSymbol;
        }

        public NonTerminal GetNonTerminal(string name)
        {
            return this._startSymbol.GetNonTerminal(name);
        }


        /// <summary>
        /// Determines that value is grammar start symbol expression
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsExpression(string value)
        {
            return this._startSymbol.IsExpression(value.ToLower());
        }

        public bool IsNonTerminal(string nonTerminalName, string value)
        {
            return this._startSymbol.IsNonTerminal(nonTerminalName, value.ToLower());
        }
    }
}
