using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class Terminal : GrammarItem
    {
        public string Symbol { get; private set; }
        public Terminal(string value)
        {
            this.Symbol = value;
        }

        public virtual bool Is(string value)
        {
            throw new NotImplementedException();
        }

        public virtual int GetLength()
        {
            return this.Symbol.Length;
        }
    }
}
