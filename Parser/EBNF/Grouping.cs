using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class Grouping : GrammarItem
    {
        public const string notation = "{";

        public string Symbol { get { return Grouping.notation; } }

        public List<GrammarItem> Items { get; private set; }

        public Grouping(List<GrammarItem> items)
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
            foreach (GrammarItem item in this.Items)
                length += item.GetLength();
            return length;
        }
    }
}
