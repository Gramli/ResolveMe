using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class Termination : GrammarItem
    {
        public const string notation = ";";

        public string Symbol { get { return Alternation.notation; } }
        public GrammarItem Left { get; private set; }

        public Termination(GrammarItem left)
        {
            this.Left = left;
        }

        public bool Is(string value)
        {
            return this.Left.Is(value);
        }

        public int GetLength()
        {
            return this.Symbol.Length;
        }
    }
}
