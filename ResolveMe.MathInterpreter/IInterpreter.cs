using ResolveMe.MathCompiler;

namespace ResolveMe.MathInterpreter
{
    public interface IInterpreter
    {
        double Interpret(INotation notation);
    }
}
