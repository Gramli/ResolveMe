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
        public const string Definition = "=";
        /// <summary>
        /// NonTerminal value on right side
        /// </summary>
        private readonly IEBNFItem _rightSide;

        /// <summary>
        /// NonTerminal Name, left side of definition
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
            this._rightSide = rightSide;
        }

        public virtual string Rebuild()
        {
            return  $"{this.Name}{NonTerminal.Definition}{this._rightSide.Rebuild()}";
        }

        public virtual bool Is(string value)
        {
            return this._rightSide.Is(value);
        }
    }
}
