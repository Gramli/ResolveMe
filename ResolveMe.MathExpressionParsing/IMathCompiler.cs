using ResolveMe.MathCompiler.Notations;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler
{
    public interface IMathCompiler
    {
        InfixNotation CompileToInfix(string expression);

        PostfixNotation CompileToPostfix(string expression);

        PrefixNotation CompileToPrefix(string expression);

        IEnumerable<IExpressionToken> GetRawNotation(string expression);
    }
}
