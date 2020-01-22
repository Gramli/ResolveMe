using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    /// <summary>
    /// Group production rule in EBNF
    /// </summary>
    public interface IGroupProductionRule : IProductionRule
    {
        /// <summary>
        /// End notation of production rule in EBNF
        /// </summary>
        string EndNotation { get; }
        /// <summary>
        /// Items in production rule
        /// </summary>
        List<IEBNFItem> Items { get; }
    }
}
