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
                var subI = i + 1;
                var restOfValue = value.Substring(subI, value.Length - subI);
                bool left = this._left.Is(builder.ToString()) || this._left is Optional;

                throw new NotImplementedException();

                if (this._left.Is(builder.ToString()) && this._right.Is(restOfValue)) //TODO DAN AND NEPLATI PRO OPTIONAL
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
