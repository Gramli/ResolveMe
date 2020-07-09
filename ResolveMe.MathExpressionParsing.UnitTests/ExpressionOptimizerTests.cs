using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.UnitTests
{
    [TestClass]
    public class ExpressionOptimizerTests
    {
        private readonly ExpressionOptimizer2 expressionOptimizer2;
        public ExpressionOptimizerTests()
        {
            this.expressionOptimizer2 = new ExpressionOptimizer2(15);
        }

        [TestMethod]
        public void DoNotOptimizeExpression()
        {
            var expression = "a+ b +c+ d                       ";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            Assert.AreEqual(result.ExpressionTokens.Count, 1);
            Assert.AreEqual(result.VariableTokens.Count, 0);
        }

        [TestMethod]
        public void OptimizeExpressionUsingOperator()
        {
            var expression = "a+ b+c+d+a+b+c+d+e+s+dsa+a";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            Assert.AreEqual(result.ExpressionTokens.Count, 11);
            Assert.AreEqual(result.VariableTokens.Count, 0);
        }

        [TestMethod]
        public void OptimizeExpressionUsingFunctionArguments()
        {
            var expression = "sint(a+ b,c+(d+a+b),c+d+e+s+dsa+a)";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            Assert.AreEqual(result.ExpressionTokens.Count, 1);
            Assert.AreEqual(result.VariableTokens.Count, 2);
        }
    }
}
