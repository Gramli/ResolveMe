using System.Text;

namespace ResolveMe.FormalGrammarParsing.EBNF.EBNFItems.ProductionRuleElements
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

        public bool IsOptional => true;

        private readonly IEBNFItem _item;

        public Optional(IEBNFItem item)
        {
            this._item = item;
        }

        public bool Is(string value)
        {
            return this._item.Is(value);
        }

        public string Rebuild()
        {
            return $"{this.Notation}{this._item.Rebuild()}{this.EndNotation}";
        }
    }
}
