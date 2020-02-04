using Parser.EBNF;
using System.Collections.Generic;
using System.Linq;
using Parser.ExpressionTokens;
using System;

namespace Parser.EBNF
{
    /// <summary>
    /// Mathematical expression parser using Extended Backus Naus form grammar
    /// </summary>
    public class EBNFMathExpressionParser
    {
        private readonly EBNFMathGrammar _grammar;

        public EBNFMathExpressionParser(EBNFMathGrammar grammar)
        {
            this._grammar = grammar;
        }

        public void ReadExpression(string expression)
        {
            var result = new Queue<IToken>();
            UndefinedToken firstUndefined = null;

            for (var i = 0; i < expression.Length; i++)
            {
                var item = expression[i];
                //take first token
                var nextToken = CreateToken(item);
                switch (nextToken)
                {
                    case IToken braToken when braToken is BracketToken:
                    {
                        if (i == 0)
                            result.Enqueue(nextToken);
                        else
                        {
                            var tokenBefore = CreateToken(expression[i - 1]);
                            //can be digit or letter
                            if (tokenBefore is UndefinedToken)
                            {
                                //potencially is function
                                //need to be check if is function, if not throws exception
                            }
                            else result.Enqueue(nextToken);
                        }
                        
                    }
                        break;
                    case IToken opToken when opToken is OperatorToken:
                    {
                        if (firstUndefined != null)
                        {
                            //find nonTerminal before operator
                            var nonTerminal = this._grammar.Recognize(firstUndefined.Value);
                            //check if find nonTerminal and if nonTerminal is expression
                            if (nonTerminal != null && _grammar.IsExpression(firstUndefined.Value))
                            {
                                switch (nonTerminal.Name)
                                {
                                    case string number when number.Equals(this._grammar.NumberNonTerminalName):
                                        result.Enqueue(new NumberToken(firstUndefined.Value));
                                        break;
                                    case string variable when variable.Equals(this._grammar.VariableNonTerminalName):
                                        result.Enqueue(new VariableToken(firstUndefined.Value));
                                        break;
                                    case string prefix
                                        when prefix.Equals(this._grammar.PrefixFuncNonTerminalName): /*TODO*/ break;
                                    case string infix
                                        when infix.Equals(this._grammar.InfixFuncNonTerminalName): /*TODO*/ break;
                                }
                            }
                            else
                                throw new Exception(
                                    $"Parse error. Can't parse character which start at {i} index expression.");
                        }
                        else
                            throw new Exception(
                                $"Parse error. Can't find any character before operator. Expression index {i}.");
                    }
                        break;
                    case IToken valToken when valToken is UndefinedToken:
                    {
                        if (firstUndefined != null)
                            firstUndefined.Concat((UndefinedToken) nextToken);
                        else
                            firstUndefined = new UndefinedToken((string) nextToken.GetValue());
                    }
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