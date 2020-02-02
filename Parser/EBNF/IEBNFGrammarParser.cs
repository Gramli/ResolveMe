namespace Parser.EBNF
{
    public interface IEBNFGrammarParser
    {
        StartSymbol Parse(string grammar);
    }
}
