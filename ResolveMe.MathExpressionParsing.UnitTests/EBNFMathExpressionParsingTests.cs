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
            EBNFGrammarParserCustom parser = new EBNFGrammarParserCustom(80);
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
            Assert.IsTrue(grammar.IsExpression("-sin(0.3)*3+(-9.9877851)/max(var1,1,144,47,var1,sin(max(2474,var1)))"));
            Assert.IsTrue(grammar.IsExpression("-cos(0.9)*3+(-12.987)/min(25,1,14,47,87,sin(cos(24,64)))"));
            Assert.IsTrue(grammar.IsExpression("-argsin(0.9)*456+(-12.987)/log(25,1,48,654,87,sin(ln(24,64)))"));
        }
    }
}
