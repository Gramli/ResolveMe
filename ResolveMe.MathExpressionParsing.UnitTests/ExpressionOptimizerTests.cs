using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathCompiler.Algorithms;
using System.Linq;

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
        public void OptimizeExpressionUsingArguments()
        {
            var expression = "(sint(a+ b,c+(d+a+b),c+d+e+s+dsa+a))";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            Assert.AreEqual(result.ExpressionTokens.Count, 1);
            Assert.AreEqual(result.VariableTokens.Count, 2);
        }

        [TestMethod]
        public void OptimizeExpressionUsingRecursiveFunction()
        {
            var expression = "(sint(cos(max(1,2))))";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            CheckRecursiveExpressions(2, result);
        }

        [TestMethod]
        public void OptimizeExpressionUsingRecursiveFunction_1()
        {
            var expression = "sin(cos(0,5))";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            CheckRecursiveExpressions(1, result);
        }

        [TestMethod]
        public void OptimizeExpressionUsingRecursiveFunction_2()
        {
            var expression = "sin(cos(sin(cos(0.1))))";
            var result = expressionOptimizer2.OptimizeExpression(expression);
            CheckRecursiveExpressions(3, result);
        }

        private void CheckRecursiveExpressions(int recursionCount, OptimizerResult result)
        {
            if(recursionCount == 0)
            {
                return;
            }

            for(var i=0;i<recursionCount;i++)
            {
                Assert.AreEqual(result.ExpressionTokens.Count, 1);
                Assert.AreEqual(result.VariableTokens.Count, 1);
                CheckRecursiveExpressions(recursionCount - 1, result.VariableTokens.First().Value);
            }
        }
    }
}
