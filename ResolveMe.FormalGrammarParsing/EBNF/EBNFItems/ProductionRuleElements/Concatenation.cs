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

        public bool IsOptional => this._left.IsOptional && this._right.IsOptional;

        private readonly IEBNFItem _left;

        private readonly IEBNFItem _right;

        public Concatenation(IEBNFItem left, IEBNFItem right)
        {
            this._left = left;
            this._right = right;
        }

        public bool Is(string value)
        {
            var isNullOrEmpty = string.IsNullOrEmpty(value);
            var result = isNullOrEmpty && this._left.IsOptional && this._right.IsOptional ||
                this._right.IsOptional && this._left.Is(value) || this._left.IsOptional && this._right.Is(value);

            if (!result && !isNullOrEmpty)
            {
                var actualValue = string.Empty;
                for (int i = 0; i < value.Length - 1; i++)
                {
                    actualValue += value[i];
                    var ii = i + 1;
                    var restOfValue = value.Substring(ii, value.Length - ii);
                    if (this._left.Is(actualValue) && this._right.Is(restOfValue))
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
    }
}
