using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Parser
{
    public class EBNFParser
    {
        public const string definition = " = ";
        public const string concatenation = " , ";
        public const string termination = " ;";
        public const string alternation = " | ";
        public const string optional = "[]";
        public const string repetition = "{}";
        public const string grouping = "()";


        public string Grammar { get; private set; }
        public EBNFParser(string grammar)
        {
            Parse(this.Grammar);
        }

        private void Parse(string grammar)
        {
            List<NonTerminal> result = new List<NonTerminal>();

            this.Grammar = grammar;
            string[] grammarItems = grammar.Split(EBNFParser.termination);
            foreach (string item in grammarItems)
            {
                NonTerminal nonTerminal = GetNonTerminal(item, result);
                result.Add(nonTerminal);
            }
        }

        private NonTerminal GetNonTerminal(string production, List<NonTerminal> listOfExistedTerminals)
        {
            string[] nonTerminalProductionRule = production.Split(EBNFParser.definition);
            GrammarItem nonTerminalRule = GetGrammarItem(nonTerminalProductionRule[1], listOfExistedTerminals);
            NonTerminal result = new NonTerminal(nonTerminalProductionRule[0], nonTerminalRule);
            return result;

        }

        private GrammarItem GetGrammarItem(string rule, List<NonTerminal> listOfExistedTerminals)
        {
            GrammarItem result = null;
            GrammarItem left = GetFirstGrammarItem(rule, listOfExistedTerminals);
            string restOfRule = rule.Substring(0, left.GetLength());
            string firstChar = restOfRule[0].ToString();
            if (firstChar.Equals(Termination.notation))
                result = new Termination(left);
            else
            {
                string newRule = restOfRule.Substring(0, 1);
                switch (firstChar)
                {
                    case Alternation.notation:
                        result = new Alternation(left, GetGrammarItem(newRule, listOfExistedTerminals));
                        break;
                }
            }
            return result;
        }

        private GrammarItem GetFirstGrammarItem(string rule, List<NonTerminal> listOfExistedTerminals)
        {
            GrammarItem result = null;
            string firstChar = rule[0].ToString();
            //terminal
            if (firstChar.Equals('"'))
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 1; i < rule.Length; i++)
                {
                    if (rule[i].Equals('"'))
                        break;
                    builder.Append(rule[i]);
                }
                result = new Terminal(builder.ToString());
            }
            //nonTerminal
            else if (Regex.IsMatch(firstChar, "[a-zA-Z]"))
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < rule.Length; i++)
                {
                    if (Regex.IsMatch(rule[i].ToString(), @"[,;|\[\]\{\}\(\)]"))
                        break;
                    builder.Append(rule[i]);
                }
                result = (from item in listOfExistedTerminals where item.Symbol.Equals(builder.ToString()) select item).Single();
            }
            else if (Regex.IsMatch(firstChar, @"[\[\{\(]"))
            {
                switch(firstChar)
                {
                    case Grouping.notation:

                        break;
                }
            }
            return result;

        }
    }
}
