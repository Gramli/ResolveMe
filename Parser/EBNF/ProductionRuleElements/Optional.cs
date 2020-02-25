using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Optional rule (group)
    /// </summary>
    public class Optional : IGroupProductionRule
    {
        public const string notation = "[";

        public const string endNotation = "]";

        public string Notation => Optional.notation;
        public string EndNotation => Optional.endNotation;

        private readonly IEBNFItem _item;

        public Optional(IEBNFItem item)
        {
            this._item = item;
        }

        public bool Is(string value)
        {
            var result = false;
            if (!this._item.Is(value))
            {
                var builder = new StringBuilder();
                for (var i = 0; i < value.Length; i++)
                {
                    builder.Append(value[i]);
                    if (this._item.Is(builder.ToString()))
                    {
                        result = true;
                        break;
                    }

                }
            }
            else
                result = true;

            return result;
        }

        public string Rebuild()
        {
            return $"{this.Notation}{this._item.Rebuild()}{this.EndNotation}";
        }
    }
}
