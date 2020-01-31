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
        public string Notation => Concatenation.notation;
        private readonly IEBNFItem _left;

        private readonly IEBNFItem _right;

        public Concatenation(IEBNFItem left, IEBNFItem right)
        {
            this._left = left;
            this._right = right;
        }

        public bool Is(string value)
        {
            
        }

        public string Rebuild()
        {
            return $"{this._left.Rebuild()}{this.Notation}{this._right.Rebuild()}";
        }
    }
}
