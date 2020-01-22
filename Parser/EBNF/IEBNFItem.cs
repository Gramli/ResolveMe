using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    /// <summary>
    /// Represents item of EBNF grammar
    /// </summary>
    public interface IEBNFItem
    {
        bool Is(string value);
        /// <summary>
        /// Allows to rebuilt item like is written in grammar
        /// </summary>
        /// <returns></returns>
        string Rebuild();
    }
}
