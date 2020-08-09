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
        public void TestExpressionWithCustomFunctionType()
        {
           var sumArray = new Func<object[], object>((args) => AddConstantToArray(args));

            var inputArray = new double[] { 1, 2, 3, 4, 5, 6 };
            var constant = (double)1;

            this.calculator.Context.AddVariable("ar", inputArray);
            this.calculator.Context.AddVariable("con", constant);
            this.calculator.Context.TryAddFunction("addarray", sumArray);

            var expression = "addarray(con,ar)";
            var result = this.calculator.Calculate<double[]>(expression);

            for(var i=0;i<result.Length;i++)
            {
                Assert.AreEqual(i+2, result[i]);
            }
        }

        [TestMethod]
        public void TestExpressionWithCustomFunction()
        {
            var stdv = new Func<object[], object>((args) => StdvFunction(args));

            this.calculator.Context.TryAddFunction("stdv", stdv);

            var expressions = new Dictionary<string, double>()
            {
                { "stdv(25,1,6,15,17)", (double)8.45},
            };

            foreach (var expression in expressions)
            {
                var result = this.calculator.Calculate<double>(expression.Key);
                Assert.AreEqual(expression.Value, (double)Math.Round(result, 2, MidpointRounding.AwayFromZero));
            }
        }

        private double[] AddConstantToArray(object[] args)
        {
            var constant = (double)args[0];
            var array = (double[])args[1];

            for(var i=0;i<array.Length;i++)
            {
                array[i] += constant;
            }

            return array;
        }

        private double StdvFunction(object[] args)
        {
            this.calculator.Context.TryGetFunction("avg", out var avgFunct);
            var avg = (double)avgFunct(args);

            var result = (double)0;
            foreach (var arg in args)
            {
                if (arg is double number)
                {
                    var temp = number - avg;
                    result += Math.Pow(temp, 2);
                    continue;
                }

                throw new ArgumentException("args", "Function expects double");
            }

            return Math.Sqrt(result / (double)args.Length);
        }
    }
}
