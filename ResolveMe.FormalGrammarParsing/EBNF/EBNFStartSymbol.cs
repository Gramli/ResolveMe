using ResolveMe.FormalGrammarParsing.EBNF.EBNFItems;
using System.Collections.Generic;

namespace ResolveMe.FormalGrammarParsing.EBNF
{
    public class EBNFStartSymbol : IEBNFStartSymbol
    {
        /// <summary>
        /// Production rules cached by its names
        /// </summary>
        private readonly Dictionary<string, NonTerminal> _productionRules;

        /// <summary>
        /// Start symbol rule
        /// </summary>
        private readonly NonTerminal _startSymbolNonTerminal;

        internal EBNFStartSymbol(NonTerminal startSymbolNonTerminal, IEnumerable<NonTerminal> productionRules)
        {
            this._startSymbolNonTerminal = startSymbolNonTerminal;
            this._productionRules = new Dictionary<string, NonTerminal>();
            InicializeProductionRules(productionRules);
        }

        private void InicializeProductionRules(IEnumerable<NonTerminal> productionRules)
        {
            foreach (NonTerminal productionRule in productionRules)
                this._productionRules[productionRule.Name] = productionRule;
        }

        /// <summary>
        /// Determines that value is grammar start symbol expression
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsExpression(string value)
        {
            return this._startSymbolNonTerminal.Is(value);
        }

        public bool IsNonTerminal(string nonTerminalName, string value)
        {
            return this._productionRules[nonTerminalName].Is(value);
        }

        public NonTerminal GetNonTerminal(string name)
        {
            return this._productionRules[name];
        }
    }
}
