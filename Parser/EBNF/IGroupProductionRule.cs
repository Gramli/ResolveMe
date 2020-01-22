using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public interface IGroupProductionRule : IProductionRule
    {
        string EndNotation { get; }

        List<IEBNFItem> Items { get; }
    }
}
