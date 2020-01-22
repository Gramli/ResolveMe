using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    public class Termination : IProductionRule
    {
        public const string notation = ";";
        public string Notation { get { return Termination.notation; } }
        public string Representation { get {  } }
        public IEBNFItem Left { get; private set; }

        public Termination(IEBNFItem left)
        {
            this.Left = left;
        }

        public bool Is(string value)
        {
            return this.Left.Is(value);
        }

        public int GetLength()
        {
            return this.Representation.Length;
        }

        public string Rebuild()
        {
            return $"{this.Left.Rebuild()}{this.Representation}";
        }
    }
}
