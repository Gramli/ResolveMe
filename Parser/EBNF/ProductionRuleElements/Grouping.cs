using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    public class Grouping : IGroupProductionRule
    {
        public const string notation = "(";

        public const string endNotation = "(";
        public string Notation { get { return Grouping.endNotation; } }
        public string EndNotation { get { return Grouping.notation; } }
        public string Representation => throw new NotImplementedException();

        public List<IEBNFItem> Items => throw new NotImplementedException();

        public int GetLength()
        {
            throw new NotImplementedException();
        }

        public bool Is(string value)
        {
            throw new NotImplementedException();
        }

        public string Rebuild()
        {
            throw new NotImplementedException();
        }
    }
}
