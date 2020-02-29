using System.Text;

namespace ResolveMe.FormalGrammarParsing.EBNF.EBNFItems.ProductionRuleElements
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
            if (string.IsNullOrEmpty(value) && this._left.IsOptional() && this._right.IsOptional())
                result = true;
            else if (this._right.IsOptional() && this._left.Is(value) || this._left.IsOptional() && this._right.Is(value))
                result = true;
            else if (!string.IsNullOrEmpty(value))
            {
                var builder = new StringBuilder();
                for (var i = 0; i < value.Length; i++)
                {
                    builder.Append(value[i]);
                    var subI = i + 1;
                    var restOfValue = value.Substring(subI, value.Length - subI);

                    bool leftIs = this._left.Is(builder.ToString());
                    if (leftIs && this._right.IsOptional() && string.IsNullOrEmpty(restOfValue)
                        || leftIs && this._right.Is(restOfValue))
                    {
                        result = true;
                        break;
                    }
                }

            }

            return result;
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
