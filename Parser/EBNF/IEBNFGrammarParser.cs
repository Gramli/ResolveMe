﻿namespace Parser.EBNF
{
    public interface IEBNFGrammarParser
    {
        IEBNFStartSymbol Parse(string grammar);
    }
}
