using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF.ProductionRuleElements
{
    public class Concatenation : IProductionRule
    {
        public const string notation = ",";
        public string Notation { get { return Concatenation.notation; } }
        public string Representation { get {  } }
        public IEBNFItem Left { get; private set; }

        public IEBNFItem Right { get; private set; }

        public Concatenation(IEBNFItem left, IEBNFItem right)
        {
            this.Left = left;
            this.Right = right;
        }

        public bool Is(string value)
        {
            throw new NotImplementedException();
        }

        public int GetLength()
        {
            return Representation.Length;
        }

        public string Rebuild()
        {
            return $"{this.Left.Rebuild()}{this.Representation}{this.Right.Rebuild()}";
        }
    }
}
