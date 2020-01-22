using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Grouping rule (group)
    /// </summary>
    public class Grouping : IGroupProductionRule
    {
        public const string notation = "(";

        public const string endNotation = ")";
        public string Notation { get { return Grouping.notation; } }
        public string EndNotation { get { return Grouping.endNotation; } }

        public List<IEBNFItem> Items { get; private set; }

        public Grouping(List<IEBNFItem> items)
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
            result.Append(Grouping.notation);
            foreach (IEBNFItem item in this.Items)
                result.Append(item.Rebuild());
            result.Append(Grouping.endNotation);
            return result.ToString();
        }
    }
}
