using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class EquationParser
    {
        private List<Function> knownFuncts;
        public EquationParser(List<Function> knownFuncts)
        {
            this.knownFuncts.AddRange(knownFuncts);
        }

        public EquationParser(List<string> knownFuncts, char separator)
        {
            foreach(string item in knownFuncts)
            {
                this.knownFuncts.Add(new Function(item, separator));
            }
        }

        public EquationParser()
        {
            this.knownFuncts = new List<Function>();
        }

        public IEquation Parse()
        {

        }
    }
}
