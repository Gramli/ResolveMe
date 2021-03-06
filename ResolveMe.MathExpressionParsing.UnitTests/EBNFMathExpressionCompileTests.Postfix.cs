﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.ExpressionTokens;
using System;
using System.Linq;

namespace ResolveMe.UnitTests
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
        public void CompileExpression()
        {
            CheckExpression("sin(a*b)", new Type[]
            {
                typeof(VariableToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionToken)
            });

            CheckExpression("sin(a*b)+25", new Type[]
            {
                typeof(VariableToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionToken),
                typeof(NumberToken),
                typeof(OperatorToken),
            });

            CheckExpression("sin(a)", new Type[]
            {
                typeof(VariableToken),
                typeof(FunctionToken)
            });

            CheckExpression("sin(0.2)", new Type[]
            {
                typeof(NumberToken),
                typeof(FunctionToken),
            });

            CheckExpression("sin(0.2+a)", new Type[]
            {
                typeof(NumberToken),
                typeof(VariableToken),
                typeof(OperatorToken),
                typeof(FunctionToken)
            });

            CheckExpression("cos(-9.9874551)", new Type[]
            {
                typeof(SignToken),
                typeof(NumberToken),
                typeof(FunctionToken),
            });

            CheckExpression("max(25,a)", new Type[]
            {
                typeof(NumberToken),
                typeof(VariableToken),
                typeof(FunctionToken),
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
