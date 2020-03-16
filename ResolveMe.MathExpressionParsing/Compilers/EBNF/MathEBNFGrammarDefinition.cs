using Amy.Grammars.EBNF;
using Amy.Grammars.EBNF.EBNFItems;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Collections.Generic;

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

        private Dictionary<string, Type> _emptyNonTerminals = new Dictionary<string, Type>()
        {
            { "digit", typeof(StringCompiler<TextToken>) },
            { "letter", typeof(StringCompiler<TextToken>) },
            { "lesserThan", typeof(CharCompiler<OperatorToken>) },
            { "biggerThan", typeof(CharCompiler<OperatorToken>) },
            { "divide", typeof(CharCompiler<OperatorToken>) },
            { "times", typeof(CharCompiler<OperatorToken>) },
            { "minus", typeof(CharCompiler<OperatorToken>) },
            { "plus", typeof(CharCompiler<OperatorToken>) },
            { "comma", typeof(CharCompiler<CommaToken>) },
            { "r_square", typeof(CharCompiler<BracketToken>) },
            { "l_square", typeof(CharCompiler<BracketToken>) },
            { "r_round", typeof(CharCompiler<BracketToken>) },
            { "l_round", typeof(CharCompiler<BracketToken>) },
            { "sign", typeof(StringCompiler<TextToken>) },
            { "double", typeof(NumberCompiler) },
            { "word", typeof(StringCompiler<TextToken>) },
            { "variable", typeof(StringCompiler<TextToken>) },
            { "function", typeof(FunctionCompiler) },
            { "factor", typeof(FunctionCompiler) },
            { "term", typeof(FunctionCompiler) },
            { "expression", typeof(FunctionCompiler) },
        };

        public MathEBNFGrammarDefinition()
        {
        }

        private object GetInstance(Type type, string name)
        {
            return Activator.CreateInstance(type, name);
        }

        public override NonTerminal GetNewNonTerminalInstance(string name)
        {
            return (NonTerminal)GetInstance(_emptyNonTerminals[name], name);
        }
    }
}
