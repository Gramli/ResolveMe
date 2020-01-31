using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF termination rule
    /// </summary>
    public class Termination : IProductionRule
    {
        public const string notation = ";";
        public string Notation => Termination.notation;
        private readonly IEBNFItem _left;

        public Termination(IEBNFItem left)
        {
            this._left = left;
        }

        public bool Is(string value)
        {
            return this._left.Is(value);
        }

        public string Rebuild()
        {
            return $"{this._left.Rebuild()}{this.Notation}";
        }
    }
}
