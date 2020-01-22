using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Concatenation rule
    /// </summary>
    public class Concatenation : IProductionRule
    {
        public const string notation = ",";
        public string Notation { get { return Concatenation.notation; } }
        public IEBNFItem Left { get; private set; }

        public IEBNFItem Right { get; private set; }

        public Concatenation(IEBNFItem left, IEBNFItem right)
        {
            this.Left = left;
            this.Right = right;
        }

        public bool Is(string value)
        {
            throw new NotImplementedException();
        }

        public string Rebuild()
        {
            return $"{this.Left.Rebuild()}{this.Notation}{this.Right.Rebuild()}";
        }
    }
}
