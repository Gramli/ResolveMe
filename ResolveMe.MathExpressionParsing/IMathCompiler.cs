using ResolveMe.MathCompiler.Notations;

namespace ResolveMe.MathCompiler
{
    public interface IMathCompiler
    {
        InfixNotation CompileToInfix(string value);

        PostfixNotation CompileToPostfix(string value);

        PrefixNotation CompileToPrefix(string value);
    }
}
