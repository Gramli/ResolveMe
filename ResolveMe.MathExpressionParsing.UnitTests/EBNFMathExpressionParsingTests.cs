using Amy.Grammars.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.Compilers.EBNF;
using System.Diagnostics;

namespace ResolveMe.MathCompiler.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionParsingTests
    {
        public EBNFMathExpressionParsingTests()
        {
        }

        [TestMethod]
        public void EBNFMathGrammarParsePerformanceTest()
        {
            var watch = new Stopwatch();
            watch.Start();

            var parser = new EBNFGrammarParserCustom(80);
            var definition = new MathEBNFGrammarDefinition();
            var symbol = parser.Parse(definition);
            var grammar = new MathEBNFGrammarCompiler(symbol);

            watch.Stop();

            Assert.IsTrue(watch.ElapsedMilliseconds < 25);
        }

        [TestMethod]
        public void EBNFMathGrammarIsExpressionPerformanceTest()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var parser = new EBNFGrammarParserCustom(80);
            var definition = new MathEBNFGrammarDefinition();
            var symbol = parser.Parse(definition);
            var grammar = new MathEBNFGrammarCompiler(symbol);

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

            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000);
        }


        [TestMethod]
        public void EvaluateFunction()
        {

            var parser = new EBNFGrammarParserCustom(80);
            var definition = new MathEBNFGrammarDefinition();
            var symbol = parser.Parse(definition);
            var grammar = new MathEBNFGrammarCompiler(symbol);

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

        [TestMethod]
        public void CompileExpression()
        {
            var parser = new EBNFGrammarParserCustom(80);
            var definition = new MathEBNFGrammarDefinition();
            var symbol = parser.Parse(definition);
            var grammar = new MathEBNFGrammarCompiler(symbol);

            grammar.Compile("sin(a)");
        }
    }
}
