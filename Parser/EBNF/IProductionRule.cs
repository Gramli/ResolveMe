using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public interface IProductionRule : IEBNFItem
    {
        string Notation { get; }
    }
}
