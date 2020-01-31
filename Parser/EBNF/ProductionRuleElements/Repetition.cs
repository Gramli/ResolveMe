using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Repetition rule (group)
    /// </summary>
    public class Repetition : IGroupProductionRule
    {
        public const string notation = "{";
        public const string endNotation = "}";
        public string Notation => Repetition.notation;
        public string EndNotation => Repetition.endNotation;

        public List<IEBNFItem> Items { get; private set; }

        public Repetition(List<IEBNFItem> items)
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
            result.Append(this.Notation);
            foreach (IEBNFItem item in this.Items)
                result.Append(item.Rebuild());
            result.Append(this.EndNotation);
            return result.ToString();
        }
    }
}
