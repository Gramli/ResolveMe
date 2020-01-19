using System.Collections.Generic;

namespace Parser
{
    internal class ParsedExpression : IParsedExpression
    {
        public IParsedExpression Parent { get; private set; } = null;

        public string EquationString { get; private set; }

        private List<IParsedExpression> childs = null;
        public List<IParsedExpression> Childs
        {
            get
            {
                if (this.childs == null)
                    this.childs = new List<IParsedExpression>();
                return this.childs;
            }
        }

        public ParsedExpression(string equation, IParsedExpression parent)
            : this(equation)
        {
            this.Parent = parent;
        }

        public ParsedExpression(string equation)
        {
            this.EquationString = equation;
        }
    }
}
