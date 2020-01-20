using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public interface GrammarItem
    {
        string Symbol { get; }
        bool Is(string value);

        /// <summary>
        /// PREJMENOVAT
        /// </summary>
        /// <returns></returns>
        int GetLength();
    }
}
