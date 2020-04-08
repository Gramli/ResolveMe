using Amy;
using Amy.Grammars.EBNF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.Compilers.EBNF;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Diagnostics;
using System.Linq;

namespace ResolveMe.MathCompiler.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionParsingTests
    {
        IFormalGrammarParser parser;
        IFormalGrammarDefinition definition;
        IStartSymbol startSymbol;
        IFormalGrammar grammar;
        ICompiler grammarCompiler;

        public EBNFMathExpressionParsingTests()
        {
            this.parser = new EBNFGrammarParserCustom(200);
            this.definition = new MathEBNFGrammarDefinition();
            this.startSymbol = parser.Parse(definition);
            this.grammar = new MathEBNFGrammarCompiler(startSymbol);
            this.grammarCompiler = (ICompiler)this.grammar;
        }


        [TestMethod]
        public void CheckExpression()
        {
            //var ebnfStartSymbol = (EBNFStartSymbol)this.startSymbol;

            //Assert.IsTrue(ebnfStartSymbol.IsExpression("(-9.9874551)"));
            //Assert.IsTrue(grammar.IsExpression("sin(a)"));
            //Assert.IsTrue(grammar.IsExpression("sin(a+9.2)"));
            //Assert.IsTrue(grammar.IsExpression("var1"));
            //Assert.IsTrue(grammar.IsExpression("max(25,a,14,45)"));
            //Assert.IsTrue(grammar.IsExpression("sin(a+9.2)*var1+15"));
            //Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+15/max(25,1,14,47,87,7)"));
            //Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+(-9.9874551)/max(25,1,14,47,87,sin(max(24,64)))"));
            //Assert.IsTrue(grammar.IsExpression("onscreentime+(((count)-1)*0.9)"));
            //Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+(-9.9874551)/max(25,1,14,47,87,sin(max(24,64)))"));
            //Assert.IsTrue(grammar.IsExpression("-sin(0.2)*3+(-9.9874551)/max(var1,1,14,47,var1,sin(max(24,var1)))"));
            //Assert.IsTrue(grammar.IsExpression("-sin(0.3)*3+(-9.9877851)/max(var1,1,144,47,var1,sin(max(2474,var1)))"));
            //Assert.IsTrue(grammar.IsExpression("-cos(0.9)*3+(-12.987)/min(25,1,14,47,87,sin(cos(24,64)))"));
            //Assert.IsTrue(grammar.IsExpression("-argsin(0.9)*456+(-12.987)/log(25,1,48,654,87,sin(ln(24,64)))"));
        }

        [TestMethod]
        public void CompileComplexExpression()
        {
            var log = this.grammarCompiler.Compile("log(25,1,48,55,64,75)+sin(a)");//,sin(lns(24,64)))");
            //var result = this.grammarCompiler.Compile("-argsin(0.9)*456+(-12.987)/log(25,1,48,654,87,sin(lns(24,64)))");
        }

        [TestMethod]
        public void CompileFunctionsExpression()
        {
            CheckFunction("sin(a*b)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });

            CheckFunction("sin(a)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });

            CheckFunction("sin(0.2)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(RightBracketToken)
            });

            CheckFunction("sin(0.2+a)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(SignToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });

            CheckFunction("cos(-9.9874551)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(SignToken),
                typeof(NumberToken),
                typeof(RightBracketToken)
            });

            CheckFunction("max(25,a,35)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(CommaToken),
                typeof(VariableToken),
                typeof(CommaToken),
                typeof(NumberToken),
                typeof(RightBracketToken)
            });
        }

        private void CheckFunction(string expresion, Type[] argumentsTypes)
        {
            var result = this.grammarCompiler.Compile(expresion);
            Assert.IsTrue(result.Count().Equals(1));

            var functToken = result.First() as FunctionToken;
            Assert.IsTrue(functToken != null);
            Assert.IsTrue(functToken.Arguments.Count == argumentsTypes.Length);

            for(int i=0;i<functToken.Arguments.Count;i++)
            {
                Assert.IsTrue(functToken.Arguments[i].GetType().Equals(argumentsTypes[i]));
            }
        }
    }
}
