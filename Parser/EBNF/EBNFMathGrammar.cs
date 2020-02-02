﻿using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class EBNFMathGrammar : IMathExpressionGrammar
    {
        public string NumberNonTerminalName => "number";

        public string VariableNonTerminalName => "variable";

        public string InfixFuncNonTerminalName => "infix_function";

        public string PrefixFuncNonTerminalName => "prefix_function";

        private StartSymbol _startSymbol;
        private readonly IEBNFGrammarParser _parser;

        public EBNFMathGrammar(IEBNFGrammarParser parser, string grammar)
        {
            this._parser = parser;
            Parse(grammar);
           
        }

        private void Parse(string grammar)
        {
            this._startSymbol = this._parser.Parse(grammar);
            //set functions
        }

        public NonTerminal Recognize(string value)
        {
            return this._startSymbol.Recognize(value);
        }
    }
}