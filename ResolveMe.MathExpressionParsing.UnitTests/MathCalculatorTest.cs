using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathInterpreter;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.UnitTests
{
    [TestClass]
    public class MathCalculatorTest
    {
        private IMathCalculator calculator;
        public MathCalculatorTest()
        {
            this.calculator = new MathCalculator();
        }

        [TestMethod]
        public void TestExpressionWithVariables()
        {
            this.calculator.Context.AddVariable("ab", (double)10);
            this.calculator.Context.AddVariable("bc", (double)5);
            var expression = "max(25,1)+45-ab*bc+12";
            var result = this.calculator.Calculate<double>(expression);
            Assert.AreEqual((double)32, (double)result);
        }

        [TestMethod]
        public void TestComplexExpression()
        {
            var expression = "cos(0.9)*456-54+(12.987)/log10(0.5)/cos(0.2)*sin(0.6)";
            var result = this.calculator.Calculate<double>(expression);
            Assert.AreEqual((double)32, (double)result);
        }
    }
}
