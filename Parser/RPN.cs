using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Parser.EBNF.ProductionRuleElements;

namespace Parser
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