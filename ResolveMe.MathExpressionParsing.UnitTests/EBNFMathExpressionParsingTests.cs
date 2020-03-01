using Amy.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ResolveMe.MathExpressionParsing.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionParsingTests
    {
        private readonly IEBNFGrammar grammar;
        public EBNFMathExpressionParsingTests()
        {
            EBNFGrammarParserCustom parser = new EBNFGrammarParserCustom();
            string file = File.ReadAllText("Debug/EBNFGrammar.txt");
            IEBNFStartSymbol symbol = parser.Parse(file);
            this.grammar = new MathGrammarEBNF(symbol);
        }

        [TestMethod]
        public void EvaluateFunction()
        {
            Assert.IsTrue(grammar.IsExpression("(-9.9874551)"));
            Assert.IsTrue(grammar.IsExpression("sin(a)"));
            Assert.IsTrue(grammar.IsExpression("sin(a+9.2)"));
            Assert.IsTrue(grammar.IsExpression("var1"));
            Assert.IsTrue(grammar.IsExpression("max(25,a,14,45)"));
            Assert.IsTrue(grammar.IsExpression("sin(a+9.2)*var1+15"));
            Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+15/max(25,1,14,47,87,7)"));
            Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+(-9.9874551)/max(25,1,14,47,87,sin(max(24,64)))"));
            Assert.IsTrue(grammar.IsExpression("onscreentime+(((count)-1)*0.9)"));
        }
    }
}
