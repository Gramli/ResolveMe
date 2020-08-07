using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveMe.MathInterpreter;

namespace ResolveMe.UnitTests
{
    [TestClass]
    public class MathCalculatorTestCustomFunction
    {
        private readonly IMathCalculator calculator;

        public MathCalculatorTestCustomFunction()
        {
            this.calculator = new MathCalculator();
        }

        [TestMethod]
        public void TestExpressionWithCustomFunction()
        {
            var sum = new Func<object[], object>((args) => SumFunction(args));

            this.calculator.Context.TryAddFunction("sum", sum);

            var expressions = new Dictionary<string, double>()
            {
                { "sum(25,1,6,7)", (double)39},

            };

            foreach (var expression in expressions)
            {
                var result = this.calculator.Calculate<double>(expression.Key);
                Assert.AreEqual(expression.Value, (double)result);
            }
        }

        private double SumFunction(object[] args)
        {
            var result = (double) 0;
            foreach (var arg in args)
            {
                if (arg is double number)
                {
                    result += number;
                    continue;
                }

                throw new ArgumentException("args", "Function expects double");
            }

            return result;
        }
    }
}
