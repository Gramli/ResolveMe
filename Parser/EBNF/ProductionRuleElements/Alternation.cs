using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Alternation rule
    /// </summary>
    public class Alternation : IProductionRule
    {
        public const string notation = "|";

        public string Notation { get { return Alternation.notation; } }
        public IEBNFItem Left { get; private set; }

        public IEBNFItem Right { get; private set; }

        public Alternation(IEBNFItem left, IEBNFItem right)
        {
            this.Left = left;
            this.Right = right;
        }

        public bool Is(string value)
        {
            return this.Left.Is(value) || this.Right.Is(value);
        }

        public string Rebuild()
        {
            return $"{this.Left.Rebuild()}{this.Notation}{this.Right.Rebuild()}";
        }
    }
}
