using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Linq;

namespace ResolveMe.MathCompiler.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionCompileTests
    {
        IMathCompiler mathCompiler;

        public EBNFMathExpressionCompileTests()
        {
            this.mathCompiler = new MathCompilerEBNF();
        }

        //[TestMethod]
        //public void CheckExpression()
        //{
        //    var ebnfStartSymbol = (EBNFStartSymbol)this.startSymbol;

        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("(-9.9874551)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("sin(a^2)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("sin(9.2)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("var1"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("max(25,a)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("sin(9.2)*var1+15"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("-sin(0.2)*3+15/max(25,1)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("onscreentime+(((count)-1)*0.9)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("-argsin(0.9,40)*456-54+(-12.987)"));
        //    Assert.IsTrue(ebnfStartSymbol.IsExpression("log10(5)/cos(0.2)*sin(45)"));
        //}

        [TestMethod]
        public void CompileComplexExpression()
        {
            CheckExpression("max(25,1)+45-ab*bc+log5(12)", new Type[]
            {
                typeof(FunctionToken),
                typeof(OperatorToken),
                typeof(NumberToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionToken),

            });

            CheckExpression("-cos(0.9)*456-54+(-12.987)/log10(0.5)/cos(0.2)*sin(0.6)", new Type[]
            {
                typeof(SignToken),
                typeof(FunctionToken),
                typeof(OperatorToken),
                typeof(NumberToken),
                typeof(OperatorToken),
                typeof(NumberToken),
                typeof(OperatorToken),
                typeof(LeftBracketToken),
                typeof(SignToken),
                typeof(NumberToken),
                typeof(RightBracketToken),
                typeof(OperatorToken),
                typeof(FunctionToken),
                typeof(OperatorToken),
                typeof(FunctionToken),
                typeof(OperatorToken),
                typeof(FunctionToken),
            });
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
                typeof(OperatorToken),
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

            CheckFunction("max(25,a)", new Type[]
            {
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(CommaToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });
        }

        private void CheckFunction(string expresion, Type[] argumentsTypes)
        {
            var result = this.mathCompiler.CompileToInfix(expresion).ExpressionTokens;
            Assert.IsTrue(result.Count().Equals(1));

            var functToken = result.First() as FunctionToken;
            Assert.IsTrue(functToken != null);
            Assert.IsTrue(functToken.Arguments.Count == argumentsTypes.Length);

            for (int i = 0; i < functToken.Arguments.Count; i++)
            {
                Assert.IsTrue(functToken.Arguments[i].GetType().Equals(argumentsTypes[i]));
            }
        }

        private void CheckExpression(string expresion, Type[] argumentsTypes)
        {
            var result = this.mathCompiler.CompileToInfix(expresion).ExpressionTokens.ToList();
            Assert.IsTrue(result.Count().Equals(argumentsTypes.Length));


            for (int i = 0; i < result.Count; i++)
            {
                Assert.IsTrue(result[i].GetType().Equals(argumentsTypes[i]));
            }
        }
    }
}
