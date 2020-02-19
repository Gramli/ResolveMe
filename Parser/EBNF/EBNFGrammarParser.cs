using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Parser.EBNF.ProductionRuleElements;

namespace Parser.EBNF
{
    public class EBNFParser : IEBNFGrammarParser
    { 
        public StartSymbol Parse(string grammar)
        {
            var productionRules = new List<NonTerminal>();

            grammar = grammar.Replace(" ", string.Empty);
            var productionRulesStrings = SplitByTermination(grammar).Reverse().ToArray();
            for (var i = 0; i < productionRulesStrings.Length - 1; i++)
            {
                var nonTerminal = GetNonTerminal(productionRulesStrings[i], productionRules);
                productionRules.Add(nonTerminal);
            }
            var startSymbolNonTerminal = GetNonTerminal(productionRulesStrings[productionRulesStrings.Length - 1], productionRules);
            var startSymbol = new StartSymbol(startSymbolNonTerminal.Name, startSymbolNonTerminal, productionRules);
            return startSymbol;
        }

        private NonTerminal GetNonTerminal(string productionRule, List<NonTerminal> listOfExistedTerminals)
        {
            var splittedProductionRule = SplitByDefinition(productionRule);
            var nonTerminalRule = GetEBNFItem(splittedProductionRule[1], listOfExistedTerminals);
            var result = new NonTerminal(splittedProductionRule[0], nonTerminalRule);
            return result;

        }

        private string[] SplitByTermination(string productionRules)
        {
            return Regex.Split(productionRules, $"{Termination.notation}$");
        }

        private string[] SplitByDefinition(string productionRule)
        {
            var result = new string[2];
            var definitionIndex = productionRule.IndexOf(NonTerminal.Definition, StringComparison.InvariantCulture);
            result[0] = productionRule.Substring(0, definitionIndex);
            definitionIndex++;
            result[1] = productionRule.Substring(definitionIndex, productionRule.Length - definitionIndex);
            return result;
        }

        private IEBNFItem GetEBNFItem(string rule, List<NonTerminal> listOfExistedTerminals)
        {
            IEBNFItem result = null;
            var left = GetStartEBNFItem(rule, listOfExistedTerminals);
            var restOfRule = rule.Substring(0, left.Rebuild().Length);
            var firstChar = restOfRule[0].ToString();
            if (firstChar.Equals(Termination.notation))
                result = new Termination(left);
            else
            {
                var newRule = restOfRule.Substring(0, 1);
                var right = GetEBNFItem(newRule, listOfExistedTerminals);
                switch (firstChar)
                {
                    case Alternation.notation: result = new Alternation(left, right); break;
                    case Concatenation.notation: result = new Concatenation(left, right); break;
                }
            }
            return result;
        }

        /// <summary>
        /// Try to find EBNFItem which can be on right side
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="listOfExistedTerminals"></param>
        /// <returns></returns>
        private IEBNFItem GetStartEBNFItem(string rule, List<NonTerminal> listOfExistedTerminals)
        {
            IEBNFItem result = null;
            var firstChar = rule[0];
            //terminal
            if (firstChar.Equals('"'))
            {
                var builder = new StringBuilder();
                for (var i = 1; i < rule.Length; i++)
                {
                    if (rule[i].Equals('"'))
                        break;
                    builder.Append(rule[i]);
                }
                result = new Terminal(builder.ToString());
            }
            //nonTerminal
            else if (Regex.IsMatch(firstChar.ToString(), "[a-zA-Z]"))
            {
                var builder = new StringBuilder();
                foreach (var t in rule)
                {
                    if (Regex.IsMatch(t.ToString(), @"[,;|\[\]\{\}\(\)]"))
                        break;
                    builder.Append(t);
                }
                result = (from item in listOfExistedTerminals where item.Name.Equals(builder.ToString()) select item).Single();
            }
            //repetition or optional
            else if (Regex.IsMatch(firstChar.ToString(), @"[\[\{\(]"))
            {
                string restOfRepRule = rule.Substring(0, 1);
                IEBNFItem rootItem = GetEBNFItem(restOfRepRule, listOfExistedTerminals);
                switch (firstChar.ToString())
                {
                    case Repetition.notation:
                        result = new Repetition(rootItem);
                        break;
                    case Optional.notation:
                        result = new Optional(rootItem);
                        break;
                }
            }
            return result;
        }
    }
}
