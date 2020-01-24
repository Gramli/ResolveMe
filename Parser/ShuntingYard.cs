using Parser.EBNF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    /// <summary>
    /// Shunting-yard algorithm
    /// Mthod for parsing mathematical expressions specified in infix notation.
    /// </summary>
    public class ShuntingYard
    {
        private StartSymbol startSymbolOfEBNF;

        private Dictionary<string, int> precedences = new Dictionary<string, int>()
        {
            {"+", 0},
            {"-", 0},
            {"*", 1},
            {"/", 1},
            {"^", 2},
        };

        public ShuntingYard(StartSymbol startSymbolOfEBNF)
        {
            this.startSymbolOfEBNF = startSymbolOfEBNF;
        }

        public RPN CreateRPNNotation(string infixExpression)
        {
            string start = string.Empty;
            for(int i=0; i<infixExpression.Length;i++)
            {
                start += infixExpression[i];

                List<string> ruleNames = this.startSymbolOfEBNF.Recognize(start);

                
            }
        }
    }
}
