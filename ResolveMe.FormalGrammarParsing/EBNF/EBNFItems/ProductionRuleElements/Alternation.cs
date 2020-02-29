using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.FormalGrammarParsing.EBNF.EBNFItems.ProductionRuleElements
{
    /// <summary>
    /// EBNF Alternation rule
    /// </summary>
    public class Alternation : IProductionRule
    {
        public const string notation = "|";

        public string Notation => Alternation.notation;

        private readonly IEBNFItem _left;

        private readonly IEBNFItem _right;

        public Alternation(IEBNFItem left, IEBNFItem right)
        {
            this._left = left;
            this._right = right;
        }

        public bool Is(string value)
        {
            //in case of recursion
            return this._right.Is(value) || this._left.Is(value);
        }

        public string Rebuild()
        {
            return $"{this._left.Rebuild()}{this.Notation}{this._right.Rebuild()}";
        }

        public bool IsOptional()
        {
            return false;
        }
    }
}
