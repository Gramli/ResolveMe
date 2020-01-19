using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    internal class Function
    {
        public string Name { get; private set; }

        public bool Implicit { get; private set; }

        public int Arguments { get; private set; }

        public char StartChar { get; private set; }

        public char EndChar { get; private set; }

        public string SourceText { get; }

        public int Priority { get; }
        public Function(string text, char separator)
        {
            this.SourceText = text;
            Parse(text, separator);
        }

        private void Parse(string text, char separator)
        {
            string[] data = text.Split(separator);
        }

        public bool IsInEquation(string equation)
        {
            throw new NotImplementedException();
        }
    }
}
