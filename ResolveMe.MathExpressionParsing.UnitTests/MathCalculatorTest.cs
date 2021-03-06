﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathInterpreter;
using System;
using System.Collections.Generic;

namespace ResolveMe.UnitTests
{
    [TestClass]
    public class MathCalculatorTest
    {
        private readonly IMathCalculator calculator;
        public MathCalculatorTest()
        {
            this.calculator = new MathCalculator();
        }

        [TestMethod]
        public void TestExpressionWithVariables()
        {
            this.calculator.Context.AddVariable("ab", (double)10);
            this.calculator.Context.AddVariable("bc", (double)5);
            this.calculator.Context.AddVariable("cd", (double)25);
            var expressions = new Dictionary<string, double>()
            {
                { "max(25,1)+45-ab*bc+12", (double)32},
                { "max(25,12)+45-ab*bc+log10(1)", (double)20 },
                { "max(10+2,5*(2+4),10+5-4)", (double)30 },
                { "max(ab+2,bc*(2+4),ab+bc-4)", (double)30 },
                { "min(ab,bc,cd)", (double)5 },
                { "5%2", (double)1 }

            };
            foreach (var expression in expressions)
            {
                var result = this.calculator.Calculate<double>(expression.Key);
                Assert.AreEqual(expression.Value, (double)result);
            }
        }

        [TestMethod]
        public void TestComplexExpression()
        {
            var expression = "cos(0.9)*456-54+(12.987)/log10(0.5)/cos(0.2)*sin(0.6)";
            var result = this.calculator.Calculate<double>(expression);
            Assert.AreEqual((double)204.599, (double)Math.Round(result, 3, MidpointRounding.AwayFromZero));
        }

        [TestMethod]
        public void TestInnerExpression()
        {
            var expression = "cos(24-23.8*0.2)";
            var result = this.calculator.Calculate<double>(expression);
            Assert.AreEqual((double)0.925, (double)Math.Round(result, 3, MidpointRounding.AwayFromZero));
        }

        [TestMethod]
        public void TestInnerFunction()
        {
            var expression = "max(cos(24-23.8*0.2),sin(1))";
            var result = this.calculator.Calculate<double>(expression);
            Assert.AreEqual((double)0.925, (double)Math.Round(result, 3, MidpointRounding.AwayFromZero));
        }

        [TestMethod]
        public void TestRecursiveInnerFunction()
        {
            var expression = "sin(cos(sin(cos(max(0.1,0.01)))))";
            var result = this.calculator.Calculate<double>(expression);
            Assert.AreEqual((double)0.62, (double)Math.Round(result, 3, MidpointRounding.AwayFromZero));
        }
    }
}
