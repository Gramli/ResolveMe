using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Optional rule (group)
    /// </summary>
    public class Optional : IGroupProductionRule
    {
        public const string notation = "[";

        public const string endNotation = "]";

        public string Notation => Optional.notation;
        public string EndNotation => Optional.endNotation;

        public List<IEBNFItem> Items { get; private set; }

        public Optional(List<IEBNFItem> items)
        {
            this.Items = items;
        }

        public bool Is(string value)
        {
            throw new NotImplementedException();
        }

        public string Rebuild()
        {
            StringBuilder result = new StringBuilder();
            result.Append(Optional.notation);
            foreach (IEBNFItem item in this.Items)
                result.Append(item.Rebuild());
            result.Append(Optional.endNotation);
            return result.ToString();
        }
    }
}
