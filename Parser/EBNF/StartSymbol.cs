using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class StartSymbol : Terminal
    {
        public IEBNFItem StartSymbolRule { get; private set; }

        private Dictionary<string, NonTerminal> productionRules;

        public StartSymbol(string name, IEBNFItem startSymbolRule, IEnumerable<NonTerminal> productionRules) 
            : base(name)
        {
            this.StartSymbolRule = startSymbolRule;
            InicializeProductionRules(productionRules);
        }

        private void InicializeProductionRules(IEnumerable<NonTerminal> productionRules)
        {
            this.productionRules = new Dictionary<string, NonTerminal>();
            foreach (NonTerminal productionRule in productionRules)
                this.productionRules[productionRule.Representation] = productionRule;
        }
    }
}
