namespace ResolveMe.FormalGrammarParsing.EBNF.EBNFItems.ProductionRuleElements
{
    public class EndRecursion : IProductionRule
    {
        private static EndRecursion _current;
        public static EndRecursion Current
        {
            get 
            {
                if (_current == null)
                    _current = new EndRecursion();
                return _current;
            }
        }

        public bool IsOptional => false;

        public string Notation => "ε";

        public bool Is(string value)
        {
            return true;
        }

        public string Rebuild()
        {
            return this.Notation;
        }
    }
}
