using System.Collections.Generic;

namespace ResolveMe.MathCompiler
{
    /// <summary>
    /// Reverse Polish notation
    /// </summary>
    public class RPN
    {
        private Queue<string> _output;
        public RPN()
        {
            this._output = new Queue<string>();
        }

        public void Enqueue(string value)
        {
            this._output.Enqueue(value);
        }
        
        public string Dequeue (string value)
        {
            return this._output.Dequeue();
        }
    }
}