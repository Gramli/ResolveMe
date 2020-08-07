using System;
using System.Collections.Generic;

namespace ResolveMe.MathInterpreter
{
    public class DefaultContext : IContext
    {
        protected readonly Dictionary<string, object> variables;

        protected readonly Dictionary<string, Func<object[], object>> functions;


        public DefaultContext()
        {
            this.variables = new Dictionary<string, object>(100);
            this.functions = new Dictionary<string, Func<object[], object>>(100);

            InicializeVariables();
            InicializeFunctions();
        }

        private void InicializeFunctions()
        {
            var plus = new Func<object[], object>((args) => (double)args[0] + (double)args[1]);
            var minus = new Func<object[], object>((args) => (double)args[0] - (double)args[1]);
            var times = new Func<object[], object>((args) => (double)args[0] * (double)args[1]);
            var divide = new Func<object[], object>((args) => (double)args[0] / (double)args[1]);
            var power = new Func<object[], object>((args) => Math.Pow((double)args[0], (double)args[1]));
            var sin = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Sin));
            var asin = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Asin));
            var cos = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Cos));
            var acos = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Acos));
            var tan = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Tan));
            var atan = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Atan));
            var ln = new Func<object[], object>((args) => OneArgumentFunc(args, Math.Log));
            var max = new Func<object[], object>((args) => ArrayArgumentCompareFunc(args, (result, value) => value > result));
            var min = new Func<object[], object>((args) => ArrayArgumentCompareFunc(args, (result, value) => value < result));

            functions.Add("sin", sin);
            functions.Add("cos", cos);
            functions.Add("asin", asin);
            functions.Add("acos", acos);
            functions.Add("tan", tan);
            functions.Add("atan", atan);
            functions.Add("min", min);
            functions.Add("max", max);
            functions.Add("ln", ln);
            functions.Add("+", plus);
            functions.Add("-", minus);
            functions.Add("*", times);
            functions.Add("/", divide);
            functions.Add("^", power);

            InicializeLogFunctions(0,1,0.1);
            InicializeLogFunctions(2,10, 1);
        }

        private void InicializeLogFunctions(double start, int max, double step)
        {
            for (var i = start; i <= max; i += step)
            {
                var i1 = Math.Truncate(Math.Round(i,1) * 10) / 10;
                var logFunc = new Func<double, double>((arg) => Math.Log(arg, i1));
                var log = new Func<object[], object>((args) => OneArgumentFunc(args, logFunc));
                var name = $"log{i1}";
                functions.Add(name, log);
            }
        }

        private void InicializeVariables()
        {
            this.variables.Add("pi", Math.PI);
            this.variables.Add("euler", Math.E);
        }

        private double OneArgumentFunc(object[] args, Func<double, double> function)
        {
            if (args.Length != 1) throw new ArgumentOutOfRangeException("args", "Function expects one argument.");
            var arg = (double)args[0];
            return function(arg);
        }

        private double ArrayArgumentCompareFunc(object[] args, Func<double, double, bool> compare)
        {
            if (args.Length < 1) throw new ArgumentOutOfRangeException("args", "Function expects at least one argument.");

            var result = (double)args[0];
            for (var i = 1; i < args.Length; i++)
            {
                var value = (double)args[i];
                if (compare(result, value)) result = value;
            }

            return result;
        }


        public void AddVariable(string name, object value)
        {
            this.variables.Add(name, value);
        }

        public bool TryAddVariable(string name, object value)
        {
            return this.variables.TryAdd(name, value);
        }

        public bool TryGetVariableValue(string name, out object value)
        {
            return this.variables.TryGetValue(name, out value);
        }

        public bool TryAddFunction(string name, Func<object[], object> function)
        {
            return this.functions.TryAdd(name, function);
        }

        public bool TryGetFunction(string name, out Func<object[], object> function)
        {
            return this.functions.TryGetValue(name, out function);
        }
    }
}
