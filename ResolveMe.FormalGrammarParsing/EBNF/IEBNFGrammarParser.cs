namespace ResolveMe.FormalGrammarParsing.EBNF
{
    public interface IEBNFGrammarParser
    {
        IEBNFStartSymbol Parse(string grammar);
    }
}
