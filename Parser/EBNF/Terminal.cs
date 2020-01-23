using System;

namespace Parser.EBNF
{
    /// <summary>
    /// Represents terminal in ENBF
    /// </summary>
    public class Terminal : IEBNFItem
    {
        /// <summary>
        /// Terminal representation - its character or string
        /// </summary>
        public string Value { get; private set; }
        public Terminal(string value)
        {
            this.Value = value;
        }

        public virtual bool Is(string value)
        {
            return this.Value.Equals(value);
        }

        /// <summary>
        /// Returns terminal in grammar
        /// </summary>
        /// <returns></returns>
        public virtual string Rebuild()
        {
            return $"\"{this.Value}\"";
        }
    }
}
