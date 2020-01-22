using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class NonTerminal : Terminal
    {
        private IEBNFItem rule;
        public NonTerminal(string name, IEBNFItem rule)
            : base(name)
        {
            this.rule = rule;
        }

        public override string Rebuild()
        {
            return base.Rebuild();
        }

        public override bool Is(string value)
        {
            return this.rule.Is(value);
        }
    }
}
