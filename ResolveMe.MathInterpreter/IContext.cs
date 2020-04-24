using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathInterpreter
{
    public interface IContext
    {
        void AddVariable(string name, object value);
        bool TryAddVariable(string name, object value);
        bool TryGetVariableValue(string name, out object value);
        bool TryAddFunction(string name, Func<object[], object> function);
        bool TryGetFunction(string name, out Func<object[], object> function);
    }
}
