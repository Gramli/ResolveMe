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
        public string Notation { get { return Repetition.endNotation; } }
        public string EndNotation { get { return Repetition.notation; } }

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
            result.Append(Repetition.notation);
            foreach (IEBNFItem item in this.Items)
                result.Append(item.Rebuild());
            result.Append(Repetition.endNotation);
            return result.ToString();
        }
    }
}
