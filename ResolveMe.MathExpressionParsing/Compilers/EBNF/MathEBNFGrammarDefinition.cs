using Amy.EBNF;
using Amy.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Compilers.EBNF
{
    public class MathEBNFGrammarDefinition : EBNFGrammarDefinition
    {
        private string[] _productionRules = new string[]
        {
            "expression = [ sign ], term ,{ ( sign ) , term };",
            "term = factor, { (times | divide) , factor };",
            "factor = double | variable | function | l_round, expression, r_round;",
            "function = letter, letter, word, l_round, { expression, comma }, expression, r_round;",
            "variable = letter, word;",
            "word = { letter | digit };",
            "double = [sign], digit, { digit }, [ \".\",  { digit } ] ;",
            "sign = plus | minus;",
            "l_round = \"(\" ;",
            "r_round = \")\"; ",
            "l_square = \"[\" ;",
            "r_square = \"]\" ;",
            "comma = \",\" ; ",
            "plus   =\"+\" ; ",
            "minus  =\"-\" ; ",
            "times  =\"*\" ; ",
            "divide =\"/\" ; ",
            "biggerThan = \">\" ; ",
            "lesserThan = \"<\" ; ",
            "letter = \"a\" | \"b\" | \"c\" | \"d\" | \"e\" | \"f\" | \"g\" | \"h\" | \"i\" | \"j\" | \"k\" | \"l\" | \"m\" " +
            "| \"n\" | \"o\" | \"p\" | \"q\" | \"r\" | \"s\" | \"t\" | \"u\" | \"v\" | \"w\" | \"x\" | \"y\" | \"z\" ; ",
            "digit = \"0\" | \"1\" | \"2\" | \"3\" | \"4\" | \"5\" | \"6\" | \"7\" | \"8\" | \"9\" ; ",
        };
        public override string[] ProductionRules => this._productionRules;

        private Dictionary<string, NonTerminal> _emptyNonTerminals = new Dictionary<string, NonTerminal>()
        {
            { "digit", new StringCompiler<VariableToken>("digit") },
            { "letter", new StringCompiler<VariableToken>("letter") },
            { "lesserThan", new StringCompiler<OperatorToken>("lesserThan") },
            { "biggerThan", new StringCompiler<OperatorToken>("biggerThan") },
            { "divide", new StringCompiler<OperatorToken>("divide") },
            { "times", new StringCompiler<OperatorToken>("times") },
            { "minus", new StringCompiler<OperatorToken>("minus") },
            { "plus", new StringCompiler<OperatorToken>("plus") },
            { "comma", new StringCompiler<CommaToken>("comma") },
            { "r_square", new StringCompiler<EndBracketToken>("r_square") },
            { "l_square", new StringCompiler<StartBracketToken>("l_square") },
            { "r_round", new StringCompiler<EndBracketToken>("r_round") },
            { "l_round", new StringCompiler<StartBracketToken>("l_round") },
            { "sign", new StringCompiler<OperatorToken>("sign") },
            { "double", new NumberCompiler("double") },
            { "word", new StringCompiler<VariableToken>("word") },
            { "variable", new StringCompiler<VariableToken>("variable") },
            { "function", new FunctionCompiler("function") },
            { "factor", new FunctionCompiler("factor") },
            { "term", new FunctionCompiler("term") },
            { "expression", new FunctionCompiler("expression") },
        };

        public override Dictionary<string, NonTerminal> EmptyNonTerminals => this._emptyNonTerminals;
    }
}
