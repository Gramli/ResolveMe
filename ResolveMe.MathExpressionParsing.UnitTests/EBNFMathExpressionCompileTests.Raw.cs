using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Linq;

namespace ResolveMe.UnitTests
{
    [TestClass]
    public class EBNFMathExpressionCompileTestsInfix
    {
        IMathCompiler mathCompiler;

        public EBNFMathExpressionCompileTestsInfix()
        {
            this.mathCompiler = new MathCompilerEBNF();
        }

        [TestMethod]
        public void CompileComplexExpression()
        {
            CheckExpression("max(25,1)+45-ab*bc+log5(12)", new Type[]
            {
                typeof(FunctionToken),
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
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(RightBracketToken),
            });
        }

        [TestMethod]
        public void CompileExpression()
        {
            CheckExpression("sin(a*b)", new Type[]
            {
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });

            CheckExpression("sin(a)", new Type[]
            {
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });

            CheckExpression("sin(0.2)", new Type[]
            {
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(RightBracketToken)
            });

            CheckExpression("sin(0.2+a)", new Type[]
            {
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(OperatorToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });

            CheckExpression("cos(-9.9874551)", new Type[]
            {
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(SignToken),
                typeof(NumberToken),
                typeof(RightBracketToken)
            });

            CheckExpression("max(25,a)", new Type[]
            {
                typeof(FunctionToken),
                typeof(LeftBracketToken),
                typeof(NumberToken),
                typeof(CommaToken),
                typeof(VariableToken),
                typeof(RightBracketToken)
            });
        }

        private void CheckExpression(string expresion, Type[] argumentsTypes)
        {
            var result = this.mathCompiler.GetRawNotation(expresion).ToList();
            Assert.IsTrue(result.Count().Equals(argumentsTypes.Length));


            for (int i = 0; i < result.Count; i++)
            {
                Assert.IsTrue(result[i].GetType().Equals(argumentsTypes[i]));
            }
        }
    }
}
