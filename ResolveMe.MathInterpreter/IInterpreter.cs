using ResolveMe.MathCompiler;

namespace ResolveMe.MathInterpreter
{
    public interface IInterpreter
    {
        IMathCompiler MathCompiler { get; }

        double Interpret(string exIContext context);
    }
}
