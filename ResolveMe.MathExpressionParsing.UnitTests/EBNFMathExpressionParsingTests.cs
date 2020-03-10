using Amy;
using Amy.Grammars.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.Compilers.EBNF;

namespace ResolveMe.MathCompiler.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionParsingTests
    {
        private readonly IFormalGrammar grammar;
        public EBNFMathExpressionParsingTests()
        {
            EBNFGrammarParserCustom parser = new EBNFGrammarParserCustom();
            MathEBNFGrammarDefinition definition = new MathEBNFGrammarDefinition();
            IStartSymbol symbol = parser.Parse(definition);
            this.grammar = new MathEBNFGrammarCompiler(symbol);
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
            Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+(-9.9874551)/max(25,1,14,47,87,sin(max(24,64)))"));
            Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+(-9.9874551)/max(var1,1,14,47,var1,sin(max(24,var1)))"));
        }
    }
}
