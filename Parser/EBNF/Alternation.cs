using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class Alternation : GrammarItem
    {
        public const string notation = "|";

        public string Symbol { get { return Alternation.notation; } }
        public GrammarItem Left { get; private set; }

        public GrammarItem Right { get; private set; }

        public Alternation(GrammarItem left, GrammarItem right)
        {
            this.Left = left;
            this.Right = right;
        }

        public bool Is(string value)
        {
            return this.Left.Is(value) || this.Right.Is(value);
        }

        public int GetLength()
        {
            return Symbol.Length;
        }
    }
}
