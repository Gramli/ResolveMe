using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    /// <summary>
    /// Represents start symbol in EBNF grammar
    /// </summary>
    public class StartSymbol : NonTerminal
    {
        /// <summary>
        /// Production rules cached by its names
        /// </summary>
        private Dictionary<string, NonTerminal> _productionRules;

        /// <summary>
        /// Inicialize StartSymbol
        /// </summary>
        /// <param name="name">left side of dedication</param>
        /// <param name="startSymbolRule">rule</param>
        /// <param name="productionRules">production rules in grammar</param>
        public StartSymbol(string name, IEBNFItem startSymbolRule, IEnumerable<NonTerminal> productionRules) 
            : base(name, startSymbolRule)
        {
            InicializeProductionRules(productionRules);
        }

        private void InicializeProductionRules(IEnumerable<NonTerminal> productionRules)
        {
            this._productionRules = new Dictionary<string, NonTerminal>();
            foreach (NonTerminal productionRule in productionRules)
                this._productionRules[productionRule.Name] = productionRule;
        }

        public NonTerminal Recognize(string value)
        {
            NonTerminal result = null;
            foreach(var productionRule in this._productionRules)
            {
                if (!productionRule.Value.Is(value)) continue;
                result = productionRule.Value;
                break;
            }
            return result;
        }

        public override bool Is(string value)
        {
            StringBuilder readed = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                readed.Append(value[i]);
                string actual = readed.ToString();
                

            }
        }

        /// <summary>
        /// Rebuild start symbol right side - rule
        /// </summary>
        /// <returns></returns>
        public override string Rebuild()
        {
            return base.Rebuild();
        }
    }
}
