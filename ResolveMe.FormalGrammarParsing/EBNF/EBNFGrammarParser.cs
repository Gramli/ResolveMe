using ResolveMe.FormalGrammarParsing.EBNF.EBNFItems;
using ResolveMe.FormalGrammarParsing.EBNF.EBNFItems.ProductionRuleElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ResolveMe.FormalGrammarParsing.EBNF
{
    public class EBNFGrammarParser : IEBNFGrammarParser
    {
        private readonly List<NonTerminal> _emptyRules; 
        private readonly string _termination = ";";

        public EBNFGrammarParser()
        {
            this._emptyRules = new List<NonTerminal>();
        }

        public IEBNFStartSymbol Parse(string grammar)
        {
            this._emptyRules.Clear();
            var productionRules = new List<NonTerminal>();

            grammar = grammar.Replace(" ", string.Empty);
            grammar = grammar.ToLowerInvariant();
            var productionRulesStrings = SplitByTermination(grammar).Reverse().ToArray();
            for (var i = 0; i < productionRulesStrings.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(productionRulesStrings[i])) continue;
                var nonTerminal = GetNonTerminal(productionRulesStrings[i], productionRules);
                productionRules.Add(nonTerminal);
            }
            var startSymbolNonTerminal = GetNonTerminal(productionRulesStrings[productionRulesStrings.Length - 1], productionRules);
            var startSymbol = new EBNFStartSymbol(startSymbolNonTerminal, productionRules);
            SetEmptyRules(startSymbol);
            return startSymbol;
        }

        private void SetEmptyRules(IEBNFStartSymbol startSymbol)
        {
            foreach(var rule in this._emptyRules)
            {
                IEBNFItem item = startSymbol.GetNonTerminal(rule.Name);
                rule.SetRightSide(item);
            }
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
            return Regex.Split(productionRules, $"(?<=[{this._termination}])");
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

        private IEBNFItem GetEBNFItem(string rule, List<NonTerminal> listOfExistedTerminals, string endNotation = null)
        {
            IEBNFItem result = null;
            var left = GetStartEBNFItem(rule, listOfExistedTerminals);
            var lengthOfLeftRule = left.Rebuild().Length;
            var restOfRule = rule.Substring(lengthOfLeftRule, rule.Length - lengthOfLeftRule);
            var firstChar = restOfRule[0].ToString();
            if (!string.IsNullOrEmpty(endNotation) && firstChar.Equals(endNotation))
                result = left;
            else if (IsTermination(firstChar))
                result = left;
            else
            {
                var newRule = restOfRule.Substring(1, restOfRule.Length - 1);
                var right = GetEBNFItem(newRule, listOfExistedTerminals, endNotation);
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
            else if(firstChar.ToString().Equals(EndRecursion.Current.Notation))
            {
                return EndRecursion.Current;
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
                result = (from item in listOfExistedTerminals where item.Name.Equals(builder.ToString()) select item).SingleOrDefault();
                if (result == null)
                {
                    var emptyNonTerm = new NonTerminal(builder.ToString());
                    this._emptyRules.Add(emptyNonTerm);
                    result = emptyNonTerm;
                }
            }
            //repetition or optional
            else if (Regex.IsMatch(firstChar.ToString(), @"[\[\{\(]"))
            {
                var restOfRepRule = rule.Substring(1, rule.Length - 1);
                switch (firstChar.ToString())
                {
                    case Repetition.notation:
                        var repItem = GetEBNFItem(restOfRepRule, listOfExistedTerminals, Repetition.endNotation);
                        result = new Repetition(repItem);
                        break;
                    case Optional.notation:
                        var opItem = GetEBNFItem(restOfRepRule, listOfExistedTerminals, Optional.endNotation);
                        result = new Optional(opItem);
                        break;
                }
            }
            else 
            {
                throw new Exception();
            }
            return result;
        }

        private bool IsTermination(string item)
        {
            return item.Equals(this._termination);
        }
    }
}
