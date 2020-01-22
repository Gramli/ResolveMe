using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    public class Repetition : IGroupProductionRule
    {
        public const string notation = "{";
        public const string endNotation = "{";
        public string Notation { get { return Repetition.endNotation; } }
        public string EndNotation { get { return Repetition.notation; } }
        public string Representation { get {  } }

        public List<IEBNFItem> Items { get; private set; }

        public Repetition(List<IEBNFItem> items)
        {
            this.Items = items;
        }

        public bool Is(string value)
        {
            throw new NotImplementedException();
        }
        public int GetLength()
        {
            int length = 0;
            foreach (IEBNFItem item in this.Items)
                length += item.GetLength();
            return length;
        }

        public string Rebuild()
        {
            StringBuilder result = new StringBuilder();
            foreach (IEBNFItem item in this.Items)
                result.Append(item.Rebuild());
            return result.ToString();
        }
    }
}
