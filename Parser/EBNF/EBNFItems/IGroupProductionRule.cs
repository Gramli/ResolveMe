namespace Parser.EBNF.EBNFItems
{
    /// <summary>
    /// Group production rule in EBNF
    /// </summary>
    public interface IGroupProductionRule : IProductionRule
    {
        /// <summary>
        /// End notation of production rule in EBNF
        /// </summary>
        string EndNotation { get; }
    }
}
