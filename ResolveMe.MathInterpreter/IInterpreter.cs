using ResolveMe.MathCompiler;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathInterpreter
{
    internal interface IInterpreter
    {
        object Interpret(INotation notation, IContext context);
    }
}
