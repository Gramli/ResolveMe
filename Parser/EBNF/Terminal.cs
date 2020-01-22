using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class Terminal : IEBNFItem
    {
        public string Representation { get; private set; }
        public Terminal(string value)
        {
            this.Representation = value;
        }

        public virtual bool Is(string value)
        {
            throw new NotImplementedException();
        }

        public virtual int GetLength()
        {
            return this.Representation.Length;
        }

        public virtual string Rebuild()
        {
            return $"\"{this.Representation}\"";
        }
    }
}
