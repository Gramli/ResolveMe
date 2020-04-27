using ResolveMe.MathCompiler.Notations;
using System.Collections.Generic;

namespace ResolveMe.MathCompiler
{
    public interface IMathCompiler
    {
        InfixNotation CompileToInfix(string value);

        PostfixNotation CompileToPostfix(string value);

        PrefixNotation CompileToPrefix(string value);

        IEnumerable<IExpressionToken> GetRawNotation(string expression);
    }
}
