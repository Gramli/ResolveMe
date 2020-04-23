using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Linq;

namespace ResolveMe.MathCompiler.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionCompileTestsPostfix
    {
        IMathCompiler mathCompiler;

        public EBNFMathExpressionCompileTestsPostfix()
        {
            this.mathCompiler = new MathCompilerEBNF();
        }

        [TestMethod]
        public void CompileComplexExpression()
        {
            CheckExpression("max(25,1)+45-ab*bc+log5(12)", new Type[]
            {
                typeof(NumberToken),
                typeof(NumberToken),
                typeof(FunctionNameToken),
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(CommaToken),
                typeof(NumberToken),
                typeof(RightBracketToken),
                typeof(OperatorToken),
                typeof(NumberToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionNameToken),
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(RightBracketToken),

            });

            CheckExpression("-cos(0.9)*456-54+(-12.987)/log10(0.5)/cos(0.2)*sin(0.6)", new Type[]
            {
                
            });
        }

        [TestMethod]
        public void CompileExpression()
        {
            CheckExpression("sin(a*b)", new Type[]
            {
                typeof(VariableToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionNameToken)
            });

            CheckExpression("sin(a*b)+25", new Type[]
            {
                typeof(VariableToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionNameToken),
                typeof(NumberToken),
                typeof(OperatorToken),
            });

            CheckExpression("sin(a)", new Type[]
            {
                typeof(VariableToken),
                typeof(FunctionNameToken)
            });

            CheckExpression("sin(0.2)", new Type[]
            {
                typeof(NumberToken),
                typeof(FunctionNameToken),
            });

            CheckExpression("sin(0.2+a)", new Type[]
            {
                typeof(NumberToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionNameToken)
            });

            CheckExpression("cos(-9.9874551)", new Type[]
            {
                typeof(SignToken),
                typeof(NumberToken),
                typeof(FunctionNameToken),
            });

            CheckExpression("max(25,a)", new Type[]
            {
                typeof(NumberToken),
                typeof(VariableToken),
                typeof(FunctionNameToken),
            });
        }

        private void CheckExpression(string expresion, Type[] argumentsTypes)
        {
            var result = this.mathCompiler.CompileToPostfix(expresion).ExpressionTokens.ToList();
            Assert.IsTrue(result.Count().Equals(argumentsTypes.Length));


            for (int i = 0; i < result.Count; i++)
            {
                Assert.IsTrue(result[i].GetType().Equals(argumentsTypes[i]));
            }
        }
    }
}
