using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public interface IEBNFGrammarParser
    {
        StartSymbol Parse(string grammar);
    }
}
