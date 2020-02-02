using Parser.EBNF;
using System.Collections.Generic;
using System.Linq;
using Parser.ExpressionTokens;
using System;

namespace Parser.EBNF
{
    /// <summary>
    /// </summary>
    public class EBNFMathExpressionParser
    {
        private EBNFMathGrammar _grammar;

        public EBNFMathExpressionParser(EBNFMathGrammar grammar)
        {
            this._grammar = grammar;
        }

        public void ReadExpression(string expression)
        {
            Queue<IToken> result = new Queue<IToken>();
            UndefinedToken firsUndefined = null;

            for (int i = 0; i < expression.Length; i++)
            {
                IToken nextToken = CreateToken(expression[i]);
                switch (nextToken)
                {
                    case IToken braToken when braToken is BracketToken:
                        result.Enqueue(nextToken);
                        break;
                    case IToken opToken when opToken is OperatorToken:
                        {
                            if (firsUndefined != null)
                            {
                                NonTerminal nonTerminal = this._grammar.Recognize(firsUndefined.Value);
                                //non terminal musi projit gramatikou !!
                                if (nonTerminal != null)
                                {
                                    switch (nonTerminal.Name)
                                    {
                                        case string number when number.Equals(this._grammar.NumberNonTerminalName):
                                            result.Enqueue(new NumberToken(firsUndefined.Value));
                                            break;
                                        case string variable when variable.Equals(this._grammar.VariableNonTerminalName):
                                            result.Enqueue(new VariableToken(firsUndefined.Value));
                                            break;
                                        case string prefix when prefix.Equals(this._grammar.PrefixFuncNonTerminalName): /*TODO*/ break;
                                        case string infix when infix.Equals(this._grammar.InfixFuncNonTerminalName): /*TODO*/ break;

                                    }
                                }
                            }
                            else
                                throw new Exception("Parse error, cant find any character before operator");
                        }
                        break;
                    case IToken valToken when valToken is UndefinedToken:
                        {
                            if (firsUndefined != null)
                                firsUndefined.Concat((UndefinedToken)nextToken);
                            else
                                firsUndefined = new UndefinedToken((string)nextToken.GetValue());
                        }
                        break;
                    case IToken funcToken when funcToken is FunctionToken:
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
                case '(':
                case ')':
                    result = new BracketToken(tokenChar);
                    break;
                default:
                    result = new UndefinedToken(tokenChar.ToString());
                    break;
            }
            return result;
        }
    }
}