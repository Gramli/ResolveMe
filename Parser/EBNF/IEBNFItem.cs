using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public interface IEBNFItem
    {
        string Representation { get; }
        bool Is(string value);

        /// <summary>
        /// PREJMENOVAT
        /// </summary>
        /// <returns></returns>
        int GetLength();

        string Rebuild();
    }
}
