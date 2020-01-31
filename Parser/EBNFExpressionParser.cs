using Parser.EBNF;
using System.Collections.Generic;
using System.Linq;
using Parser.ExpressionTokens;

namespace Parser
{
    /// <summary>
    /// Shunting-yard algorithm
    /// Method for parsing mathematical expressions specified in infix notation.
    /// </summary>
    public class ShuntingYard
    {
        private StartSymbol _startSymbolOfEBNF;

        public ShuntingYard(StartSymbol startSymbolOfEBNF)
        {
            this._startSymbolOfEBNF = startSymbolOfEBNF;
        }

        public void ReadExpression(string expression)
        {
            //find first undefined
            UndefinedToken actual = null;
            for (int i = 0; i < expression.Length; i++)
            {
                IToken token = CreateToken(expression[i]);
                if (!(token is UndefinedToken)) continue;
                actual = (UndefinedToken)token;
                break;
            }
            
            //continue in reading
            for (int i = 1; i < expression.Length; i++)
            {
                IToken nextToken = CreateToken(expression[i]);
                switch (nextToken)
                {
                    case IToken opToken when opToken is OperatorToken:

                        NonTerminal nonTerminal = this._startSymbolOfEBNF.Recognize(actual.Value);
                        if (nonTerminal != null)
                        {
                            
                        }
                        break;
                    case IToken valToken when valToken is UndefinedToken:
                        actual.Concat((UndefinedToken)nextToken);
                        break;
                }
            }
        }
        private IToken CreateToken(char tokenChar)
        {
            IToken result = null;
                switch (tokenChar)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        result = new OperatorToken(tokenChar.ToString());
                        break;
                    case '^': break;
                    case '(': break;
                    case ')': break;
                    default:
                        result = new UndefinedToken(tokenChar.ToString());
                        break;
                }
                return result;
        }
    }
}