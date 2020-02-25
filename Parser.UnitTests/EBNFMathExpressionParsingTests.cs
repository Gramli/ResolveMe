using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.EBNF;
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
        private string number = "number = [\" - \"], { digit }, [ \".\",  { digit } ] ;";

        [TestMethod]
        public void EvaluateOneCharExpression()
        {
            var builder = new StringBuilder();
            builder.Append("startSymbol = digit | letter | math_operator;");
            builder.Append(mathOperator);
            builder.Append(letter);
            builder.Append(digit);

            EBNFParser parser = new EBNFParser();
            StartSymbol startSymbol = parser.Parse(builder.ToString());
            Assert.IsTrue(startSymbol.Is("A"));
            Assert.IsFalse(startSymbol.Is("AB"));
        }

        [TestMethod]
        public void EvaluateWordExpression()
        {
            var builder = new StringBuilder();
            builder.Append(word);
            builder.Append(letter);
            builder.Append(digit);

            EBNFParser parser = new EBNFParser();
            StartSymbol startSymbol = parser.Parse(builder.ToString());
            Assert.IsTrue(startSymbol.Is("A"));
            Assert.IsTrue(startSymbol.Is("AB"));
            Assert.IsTrue(startSymbol.Is("AB1"));
            Assert.IsFalse(startSymbol.Is("AB1<"));
        }

        [TestMethod]
        public void EvaluateVariableExpression()
        {
            var builder = new StringBuilder();
            builder.Append(variable);
            builder.Append(word);
            builder.Append(letter);
            builder.Append(digit);

            EBNFParser parser = new EBNFParser();
            StartSymbol startSymbol = parser.Parse(builder.ToString());
            Assert.IsTrue(startSymbol.Is("A"));
            Assert.IsTrue(startSymbol.Is("AB"));
            Assert.IsTrue(startSymbol.Is("AB1"));
            Assert.IsFalse(startSymbol.Is("AB1<"));
            Assert.IsFalse(startSymbol.Is(""));
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

            EBNFParser parser = new EBNFParser();
            StartSymbol startSymbol = parser.Parse(builder.ToString());
            Assert.IsTrue(startSymbol.Is("1"));
            Assert.IsTrue(startSymbol.Is("-1"));
            Assert.IsTrue(startSymbol.Is("1.211"));
            Assert.IsTrue(startSymbol.Is("-1.211"));
            Assert.IsTrue(startSymbol.Is("12000"));
        }
    }
}
