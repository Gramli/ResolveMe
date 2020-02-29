using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.FormalGrammarParsing.EBNF;
using System.Text;

namespace ResolveMe.MathExpressionParsing.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionParsingTests
    {
        private string mathOperator = "math_operator = \" < \" | \" > \" | \" + \" | \" - \" | \" * \" | \" / \";";
        private string letter = "letter = \"a\" | \"b\" | \"c\" | \"d\" | \"e\" | \"f\" | \"g\" | \"h\" | \"i\" | \"j\" | \"k\" | \"l\" | \"m\" | \"n\" | \"o\" | \"p\" | \"q\" | \"r\" | \"s\" | \"t\" | \"u\" | \"v\" | \"w\" | \"x\" | \"y\" | \"z\" ;";
        private string digit = "digit = \"0\" | \"1\" | \"2\" | \"3\" | \"4\" | \"5\" | \"6\" | \"7\" | \"8\" | \"9\";";

        private string word = "word = { letter | digit } ;";
        private string variable = "variable = letter, word ;";
        private string number = "number = [\" - \"], digit, { digit }, [ \".\",  { digit } ] ;";
        private string functionParameter = "function_parameter = variable | number ;";

        private string infix_operation = "infix_operation = [\"(\"], infix_operation_parameter , math_operator, infix_operation_parameter,  [\")\"];";
        private string infix_operation_parameter = " infix_operation_parameter = function_parameter, infix_operation';";
        private string infix_operationDer = "infix_operation' = infix_operation' | ε ;";
        private string infix_function = "infix_function = { [\"(\"], function_parameter, math_operator, function_parameter ,  [\")\"] };";


        private string prefix_operation_parameter = "prefix_function_parameter = function_parameter | infix_operation ;";
        private string prefix_operation = "prefix_operation = letter, letter, word, \"(\", { prefix_operation_parameter | prefix_operation, \", \" }, prefix_operation_parameter | prefix_operation, \")\" ;";



        private string function = "function = { infix_operation | prefix_operation | function, math_operator, infix_operation | prefix_operation, function } ;";

        private string expression = "expression = { function | prefix_operation | infix_operation | function_parameter };";

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
            Assert.IsTrue(grammar.IsExpression("1AB1"));
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
            Assert.IsFalse(grammar.IsExpression("1ZE"));
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
            Assert.IsTrue(grammar.IsExpression("-9.9874551"));
            Assert.IsFalse(grammar.IsExpression("-9,9874551"));
        }

        [TestMethod]
        public void EvaluateFunctionParameter()
        {
            var builder = new StringBuilder();
            builder.Append(functionParameter);
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
            Assert.IsTrue(grammar.IsExpression("A"));
            Assert.IsTrue(grammar.IsExpression("ABCBC"));
            Assert.IsTrue(grammar.IsExpression("-9.9874551"));
            Assert.IsFalse(grammar.IsExpression("-9,9874551"));
            Assert.IsFalse(grammar.IsExpression("-9+9874551"));
        }

        [TestMethod]
        public void EvaluateInfixFunction()
        {
            var builder = new StringBuilder();
            builder.Append("expression =  infix_function ;");
            //builder.Append(infix_operation);
            //builder.Append(infix_operation_parameter);
            builder.Append(infix_function);
            builder.Append(functionParameter);
            builder.Append(number);
            builder.Append(variable);
            builder.Append(word);
            builder.Append(mathOperator);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsTrue(grammar.IsExpression("-9.9874551"));
            Assert.IsFalse(grammar.IsExpression("-9,9874551"));
            Assert.IsFalse(grammar.IsExpression("ABZE"));
            Assert.IsFalse(grammar.IsExpression("AB19877"));
            Assert.IsTrue(grammar.IsExpression("AB19+877"));
            Assert.IsTrue(grammar.IsExpression("AB19>877"));
            Assert.IsTrue(grammar.IsExpression("AB19>(877+15)"));
        }

        [TestMethod]
        public void EvaluatePrefixFunctionParameter()
        {
            var builder = new StringBuilder();
            builder.Append(prefix_operation_parameter);
            builder.Append(infix_operation);
            builder.Append(functionParameter);
            builder.Append(number);
            builder.Append(variable);
            builder.Append(word);
            builder.Append(mathOperator);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsTrue(grammar.IsExpression("-9.9874551"));
            Assert.IsFalse(grammar.IsExpression("-9,9874551"));
            Assert.IsTrue(grammar.IsExpression("ABZE"));
            Assert.IsTrue(grammar.IsExpression("AB19877"));
            Assert.IsTrue(grammar.IsExpression("AB19+877"));
            Assert.IsTrue(grammar.IsExpression("AB19>877"));
        }

        [TestMethod]
        public void EvaluatePrefixFunction()
        {
            var builder = new StringBuilder();
            builder.Append("expression = prefix_operation");
            builder.Append(prefix_operation);
            builder.Append(prefix_operation_parameter);
            builder.Append(infix_operation);
            builder.Append(functionParameter);
            builder.Append(number);
            builder.Append(variable);
            builder.Append(word);
            builder.Append(mathOperator);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsFalse(grammar.IsExpression("-9.9874551"));
            Assert.IsTrue(grammar.IsExpression("sin(a,b,25,14)"));
            Assert.IsFalse(grammar.IsExpression("s(a)"));
        }

        [TestMethod]
        public void EvaluateFunction()
        {
            var builder = new StringBuilder();
            builder.Append("expression = function");
            builder.Append(function);
            builder.Append(prefix_operation);
            builder.Append(prefix_operation_parameter);
            builder.Append(infix_operation);
            builder.Append(functionParameter);
            builder.Append(number);
            builder.Append(variable);
            builder.Append(word);
            builder.Append(mathOperator);
            builder.Append(letter);
            builder.Append(digit);

            IEBNFGrammarParser parser = new EBNFGrammarParser();
            IEBNFStartSymbol symbol = parser.Parse(builder.ToString());
            IEBNFGrammar grammar = new MathGrammarEBNF(symbol);
            Assert.IsFalse(grammar.IsExpression("-9.9874551"));
            Assert.IsFalse(grammar.IsExpression("sin(a)"));
            Assert.IsFalse(grammar.IsExpression("sin(a,b,25,14)"));
            Assert.IsFalse(grammar.IsExpression("sin(a+9.2)"));
            Assert.IsFalse(grammar.IsExpression("var1"));
            Assert.IsFalse(grammar.IsExpression("max(25,a,14)"));
            Assert.IsTrue(grammar.IsExpression("sin(a+9.2) *  var1 + 15"));
            Assert.IsTrue(grammar.IsExpression("sin(a+9.2) * var1 + 15 / max(25,a,14)"));
        }
    }
}
