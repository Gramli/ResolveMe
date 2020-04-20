using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.PerformanceAndOptimalization
{
    [MemoryDiagnoser]
    public class OptimizerBenchmark
    {
        [Params(15, 20, 25)] // Arguments can be combined with Params
        public uint ExpressionLength;

        [Benchmark]
        [Arguments("log10(5)/cos(0.2)*sin(45)")]
        [Arguments("-cos(0.9)*456-54+(-12.987)/log10(0.5)/cos(0.2)*sin(0.6)")]
        [Arguments("onscreentime+(((count)-1)*0.9-4564564878913)")]
        [Arguments("-argsin(0.9,40)*456-54+(-12.987)")]
        [Arguments("(-9.98745514578944321647644)")]
        public void Optimize(string value)
        {
            var optimizer = new ExpressionOptimizer(ExpressionLength);
            optimizer.SplitLongExpression(value);
        }
    }
}
