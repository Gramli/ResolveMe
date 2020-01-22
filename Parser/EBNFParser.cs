﻿using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Parser.EBNF.ProductionRuleElements;

namespace Parser
{
    public class EBNFParser
    {
        public string Grammar { get; private set; }
        public EBNFParser(string grammar)
        {
            Parse(this.Grammar);
        }

        private StartSymbol Parse(string grammar)
        {
            List<NonTerminal> productionRules = new List<NonTerminal>();

            this.Grammar = grammar.Replace(" ", string.Empty);
            string[] productionRulesStrings = SplitByTermination(this.Grammar).Reverse().ToArray();
            for (int i = 0; i < productionRulesStrings.Length - 1; i++)
            {
                NonTerminal nonTerminal = GetNonTerminal(productionRulesStrings[i], productionRules);
                productionRules.Add(nonTerminal);
            }
            NonTerminal startSymbolNonTerminal = GetNonTerminal(productionRulesStrings[productionRulesStrings.Length - 1], productionRules);
            StartSymbol startSymbol = new StartSymbol(startSymbolNonTerminal.Name, startSymbolNonTerminal, productionRules);
            return startSymbol;
        }

        private NonTerminal GetNonTerminal(string productionRule, List<NonTerminal> listOfExistedTerminals)
        {
            string[] splittedProductionRule = SplitByDefinition(productionRule);
            IEBNFItem nonTerminalRule = GetEBNFItem(splittedProductionRule[1], listOfExistedTerminals);
            NonTerminal result = new NonTerminal(splittedProductionRule[0], nonTerminalRule);
            return result;

        }

        private string[] SplitByTermination(string productionRules)
        {
            return Regex.Split(productionRules, $"{Termination.notation}$");
        }

        private string[] SplitByDefinition(string productionRule)
        {
            string[] result = new string[2];
            int definitionIndex = productionRule.IndexOf(NonTerminal.definition);
            result[0] = productionRule.Substring(0, definitionIndex);
            definitionIndex++;
            result[1] = productionRule.Substring(definitionIndex, productionRule.Length - definitionIndex);
            return result;
        }

        private IEBNFItem GetEBNFItem(string rule, List<NonTerminal> listOfExistedTerminals)
        {
            IEBNFItem result = null;
            IEBNFItem left = GetStartEBNFItem(rule, listOfExistedTerminals);
            string restOfRule = rule.Substring(0, left.Rebuild().Length);
            string firstChar = restOfRule[0].ToString();
            if (firstChar.Equals(Termination.notation))
                result = new Termination(left);
            else
            {
                string newRule = restOfRule.Substring(0, 1);
                IEBNFItem right = GetEBNFItem(newRule, listOfExistedTerminals);
                switch (firstChar)
                {
                    case Alternation.notation: result = new Alternation(left, right); break;
                    case Concatenation.notation: result = new Concatenation(left, right); break;
                }
            }
            return result;
        }

        /// <summary>
        /// Try to find EBNFItem which can can be on right side
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="listOfExistedTerminals"></param>
        /// <returns></returns>
        private IEBNFItem GetStartEBNFItem(string rule, List<NonTerminal> listOfExistedTerminals)
        {
            IEBNFItem result = null;
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
                result = (from item in listOfExistedTerminals where item.Name.Equals(builder.ToString()) select item).Single();
            }
            else if (Regex.IsMatch(firstChar, @"[\[\{\(]"))
            {

                switch (firstChar)
                {
                    case Repetition.notation:
                        break;
                    case Optional.notation:
                        break;
                    case Grouping.notation:
                        break;
                }
            }
            return result;

        }

        private List<NonTerminal> GetProductionRules(string endString)
        {
            throw new NotImplementedException();
        }
    }
}