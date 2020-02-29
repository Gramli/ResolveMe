using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.EBNF;
using Parser.EBNF.EBNFItems;
using System.IO;
using System.Text;

namespace Parser.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionParsingTests
    {
        private string mathOperator = "math_operator = \" < \" | \" > \" | \" + \" | \" - \" | \" * \" | \" / \";";
        private string letter = "letter = \"A\" | \"B\" | \"C\" | \"D\" | \"E\" | \"F\" | \"G\" | \"H\" | \"I\" | \"J\" | \"K\" | \"L\" | \"M\" | \"N\" | \"O\" | \"P\" | \"Q\" | \"R\" | \"S\" | \"T\" | \"U\" | \"V\" | \"W\" | \"X\" | \"Y\" | \"Z\" | \"a\" | \"b\" | \"c\" | \"d\" | \"e\" | \"f\" | \"g\" | \"h\" | \"i\" | \"j\" | \"k\" | \"l\" | \"m\" | \"n\" | \"o\" | \"p\" | \"q\" | \"r\" | \"s\" | \"t\" | \"u\" | \"v\" | \"w\" | \"x\" | \"y\" | \"z\" ;";
        private string digit = "digit = \"0\" | \"1\" | \"2\" | \"3\" | \"4\" | \"5\" | \"6\" | \"7\" | \"8\" | \"9\";";

        private string word = "word = { letter | digit } ;";
        private string variable = "variable = letter, word ;";
        private string functionParameter = "function_parameter = variable | number ;";
        private string number = "number = [\" - \"], digit, { digit }, [ \".\",  { digit } ] ;";

        [TestMethod]
        public void EvaluateOneCharExpression()
        {
            var builder = new StringBuilder();
            builder.Append("startSymbol = digit | letter | math_operator;");
            builder.Append(mathOperator);
            builder.Append(letter);
            builder.Append(digit);

            EBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsTrue(grammar.IsExpression("A"));
            Assert.IsFalse(grammar.IsExpression("AB"));
        }

        [TestMethod]
        public void EvaluateWordExpression()
        {
            var builder = new StringBuilder();
            builder.Append(word);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsTrue(grammar.IsExpression("A"));
            Assert.IsTrue(grammar.IsExpression("AB"));
            Assert.IsTrue(grammar.IsExpression("ABRAKADABRA12"));
            Assert.IsFalse(grammar.IsExpression("AB1<"));
        }

        [TestMethod]
        public void EvaluateVariableExpression()
        {
            var builder = new StringBuilder();
            builder.Append(variable);
            builder.Append(word);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsTrue(grammar.IsExpression("A"));
            Assert.IsTrue(grammar.IsExpression("AB"));
            Assert.IsTrue(grammar.IsExpression("AB1"));
            Assert.IsFalse(grammar.IsExpression("AB1<"));
            Assert.IsFalse(grammar.IsExpression(""));
        }

        [TestMethod]
        public void EvaluateNumberExpression()
        {
            var builder = new StringBuilder();
            builder.Append(number);
            builder.Append(variable);
            builder.Append(word);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsTrue(grammar.IsExpression("1"));
            Assert.IsTrue(grammar.IsExpression("-1"));
            Assert.IsTrue(grammar.IsExpression("1.211"));
            Assert.IsTrue(grammar.IsExpression("-1.211"));
            Assert.IsTrue(grammar.IsExpression("12000"));
            Assert.IsFalse(grammar.IsExpression("A"));
            Assert.IsFalse(grammar.IsExpression("ABCBC"));
        }
    }
}
