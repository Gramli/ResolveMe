using Amy;
using Amy.Grammars.EBNF;
using BenchmarkDotNet.Attributes;
using ResolveMe.MathCompiler.Compilers.EBNF;

namespace ResolveMe.MathCompiler.PerformanceAndOptimalization
{
    [MemoryDiagnoser]
    public class CompileBenchmark
    {
        EBNFGrammarParserCustom parser;
        MathEBNFGrammarDefinition definition;
        MathEBNFGrammarCompiler grammar;

        [Benchmark]
        [Arguments("log10(5)/cos(0.2)*sin(45)")]
        [Arguments("-cos(0.9)*456-54+(-12.987)/log10(0.5)/cos(0.2)*sin(0.6)")]
        [Arguments("onscreentime+(((count)-1)*0.9-4564564878913)")]
        [Arguments("-argsin(0.9,40)*456-54+(-12.987)")]
        [Arguments("(-9.98745514578944321647644)")]
        public void Compile(string value)
        {
            grammar.Compile(value);
        }

        [Benchmark]
        public IStartSymbol Parse()
        {
            return parser.Parse(definition);
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            parser = new EBNFGrammarParserCustom(50);
            definition = new MathEBNFGrammarDefinition();
            var startSymbol = parser.Parse(definition);
            grammar = new MathEBNFGrammarCompiler(startSymbol);
        }
    }
}
