using ResolveMe.MathCompiler;
using ResolveMe.MathCompiler.Notations;
using System.Collections.Generic;

namespace ResolveMe.MathInterpreter
{
    public interface IMathCalculator
    {
        T Calculate<T>(string expression);

        T Calculate<T>(params string[] expressions);

        PostfixNotation GetPostifixNotation(string expression);

        InfixNotation GetInfixNotation(string expression);

        PrefixNotation GetPrefixNotation(string expression);

        IEnumerable<IExpressionToken> GetRawNotation(string expression);
    }
}
