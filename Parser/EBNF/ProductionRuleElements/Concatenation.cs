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
            var result = false;
            var builder = new StringBuilder();
            for(var i=0; i<value.Length;i++)
            {
                builder.Append(value[i]);
                var restOfValue = value.Substring(i, value.Length - i);
                if (this._left.Is(builder.ToString()) && this._right.Is(restOfValue))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public string Rebuild()
        {
            return $"{this._left.Rebuild()}{this.Notation}{this._right.Rebuild()}";
        }
    }
}
