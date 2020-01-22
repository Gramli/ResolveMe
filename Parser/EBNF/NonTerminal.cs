using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    /// <summary>
    /// Represents NonTerminal in EBNF
    /// </summary>
    public class NonTerminal : IEBNFItem
    {
        public const string definition = "=";
        /// <summary>
        /// NonTerminal value on right side
        /// </summary>
        private IEBNFItem rightSide;

        /// <summary>
        /// NonTerminal Name, right side of definition
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// inicialize name and item ->right and left side
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rightSide"></param>
        public NonTerminal(string name, IEBNFItem rightSide)
        {
            this.Name = name;
            this.rightSide = rightSide;
        }

        public virtual string Rebuild()
        {
            return  $"{this.Name}{NonTerminal.definition}{this.rightSide.Rebuild()}";
        }

        public virtual bool Is(string value)
        {
            rightSide.
        }
    }
}
