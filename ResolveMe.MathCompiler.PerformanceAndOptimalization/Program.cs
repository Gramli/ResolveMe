using Amy.Grammars.EBNF;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ResolveMe.MathCompiler.Compilers.EBNF;
using System;

namespace ResolveMe.MathCompiler.PerformanceAndOptimalization
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CompilePerformance>();
        }
    }
}
