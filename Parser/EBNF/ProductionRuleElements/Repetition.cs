using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    /// <summary>
    /// EBNF Repetition rule (group)
    /// </summary>
    public class Repetition : IGroupProductionRule
    {
        public const string notation = "{";
        public const string endNotation = "}";
        public string Notation => Repetition.notation;
        public string EndNotation => Repetition.endNotation;

        private readonly IEBNFItem _item;

        public Repetition(IEBNFItem item)
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
                        if (i == value.Length - 1)
                            result = true;
                        else
                        {
                            result = this.Is(value.Substring(i, value.Length - i));
                            break;
                        }
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