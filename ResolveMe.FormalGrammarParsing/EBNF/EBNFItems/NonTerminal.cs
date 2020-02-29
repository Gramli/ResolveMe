using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.FormalGrammarParsing.EBNF.EBNFItems
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
        private IEBNFItem _rightSide;

        /// <summary>
        /// NonTerminal Name, left side of definition
        /// </summary>
        public string Name { get; private set; }

        public bool IsOptional => this._rightSide.IsOptional;

        /// <summary>
        /// inicialize name and right side
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rightSide"></param>
        public NonTerminal(string name, IEBNFItem rightSide)
            : this(name)
        {
            this._rightSide = rightSide;
        }

        public NonTerminal(string name)
        {
            this.Name = name;
        }

        internal void SetRightSide(IEBNFItem item)
        {
            this._rightSide = item;
        }

        public virtual string Rebuild()
        {
            return $"{this.Name}"; // {NonTerminal.Definition}{this._rightSide.Rebuild()}";
        }

        public virtual bool Is(string value)
        {
            return this._rightSide.Is(value);
        }
    }
}
