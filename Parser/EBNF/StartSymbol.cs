using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.EBNF
{
    public class StartSymbol : Terminal
    {
        
        public StartSymbol(string value) 
            : base(value)
        {
        }
    }
}
