using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    public class Optional : IGroupProductionRule
    {
        public const string notation = "[";

        public const string endNotation = "[";

        public string Notation => throw new NotImplementedException();
        public string EndNotation => throw new NotImplementedException();

        public List<IEBNFItem> Items => throw new NotImplementedException();

        public string Representation => throw new NotImplementedException();

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
