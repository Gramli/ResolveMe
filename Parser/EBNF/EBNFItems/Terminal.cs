using System;

namespace Parser.EBNF.EBNFItems
{
    /// <summary>
    /// Represents terminal in ENBF
    /// </summary>
    public class Terminal : IEBNFItem
    {
        /// <summary>
        /// Terminal representation - its character or string
        /// </summary>
        private readonly string _value;

        public Terminal(string value)
        {
            this._value = value;
        }

        public bool Is(string value)
        {
            return this._value.Equals(value);
        }

        /// <summary>
        /// Returns terminal in grammar
        /// </summary>
        /// <returns></returns>
        public string Rebuild()
        {
            return $"\"{this._value}\"";
        }

        public bool IsOptional()
        {
            return false;
        }
    }
}
