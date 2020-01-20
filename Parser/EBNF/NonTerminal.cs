using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class NonTerminal : Terminal
    {
        private GrammarItem rule;
        public NonTerminal(string name, GrammarItem rule)
            : base(name)
        {
            this.rule = rule;
        }

        public override bool Is(string value)
        {
            return this.rule.Is(value);
        }
    }
}
