﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class VariableToken : IToken
    {
        public string VariableName { get; private set; }

        public VariableToken()
        { 
        
        }
        public VariableToken(string variableName)
        {
            this.VariableName = variableName;
        }

        public string GetStringRepresentation()
        {
            return this.VariableName;
        }
    }
}
