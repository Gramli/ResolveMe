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
        
        public Queue<IToken> ReadExpression(string expression)
        {
            return ReadExpression(expression, null);
        }

        private Queue<IToken> ReadExpression(string expression, char? endCharacter)
        {
            //TODO check if expression
            
            var result = new Queue<IToken>();
            UndefinedToken lastUndefined = null;

            for (var i = 0; i < expression.Length; i++)
            {
                var item = expression[i];
                //take first token
                var nextToken = CreateToken(item);
                if (endCharacter.HasValue && item.Equals(endCharacter.Value))
                {
                    result.Enqueue(nextToken);
                    break;
                }

                switch (nextToken)
                {
                    case IToken braToken when braToken is StartBracketToken:
                    {
                        if (lastUndefined == null)
                            result.Enqueue(nextToken);
                        else
                        {
                            //substring with startbracket - it will add both brackets to queue
                            var newExpression = expression.Substring(i - 1, expression.Length);
                            //use recursion to find arguments of function
                            var funcArguments = ReadExpression(newExpression,
                                ((StartBracketToken) nextToken).EndBracket);
                            //create function token
                            var functionToken = new FunctionToken(lastUndefined.Value, funcArguments);
                            
                            //find nonTerminal
                            var functionStringRepresentation = functionToken.GetStringRepresentation();
                            var nonTerminal = this._grammar.Recognize(functionStringRepresentation);
                            //check if is it prefix function
                            if (nonTerminal.Name.Equals(this._grammar.PrefixFuncNonTerminalName))
                            {
                                result.Enqueue(functionToken);
                                var length = (from itemLength in funcArguments
                                    select itemLength.GetStringRepresentation().Length).Sum();
                                i += length;
                                lastUndefined = null;   
                            }
                            else
                            {
                                throw new Exception(
                                    $"Parse error. Can't recognize {functionStringRepresentation}!");
                            }
                        }
                    }
                        break;
                    case IToken commaToken when commaToken is CommaToken:
                    case IToken opToken when opToken is OperatorToken:
                    {
                        if (lastUndefined != null)
                        {
                            //find nonTerminal before operator or comma
                            var nonTerminal = this._grammar.Recognize(lastUndefined.Value);
                            //check if find nonTerminal and if nonTerminal is expression
                            if (nonTerminal != null && _grammar.IsExpression(lastUndefined.Value))
                            {
                                switch (nonTerminal.Name)
                                {
                                    case string number when number.Equals(this._grammar.NumberNonTerminalName):
                                        result.Enqueue(new NumberToken(lastUndefined.Value));
                                        lastUndefined = null;
                                        break;
                                    case string variable when variable.Equals(this._grammar.VariableNonTerminalName):
                                        result.Enqueue(new VariableToken(lastUndefined.Value));
                                        lastUndefined = null;
                                        break;
                                }

                                //add operator or comma to result
                                result.Enqueue(nextToken);
                            }
                            else
                                throw new Exception(
                                    $"Parse error. Can't parse character which start at {i} index expression.");
                        }
                        else
                        {
                            var peekedToken = result.Peek();
                            if (peekedToken == null || peekedToken is OperatorToken || peekedToken is CommaToken)
                                throw new Exception(
                                    $"Parse error. Can't find any correct character before operator or comma. Expression index {i}.");
                            //add operator or comma to result
                            result.Enqueue(nextToken);
                        }
                    }
                        break;
                    case IToken undToken when undToken is UndefinedToken undefinedToken:
                    {
                        if (lastUndefined != null)
                            lastUndefined.Concat(undefinedToken);
                        else
                            lastUndefined = new UndefinedToken(undefinedToken.GetStringRepresentation());
                    }
                        break;
                }
            }

            return result;
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
                    result = new StartBracketToken(tokenChar, ')');
                    break;
                case ')':
                    result = new EndBracketToken(tokenChar);
                    break;
                case ',':
                    result = new CommaToken(tokenChar);
                    break;
                default:
                    result = new UndefinedToken(tokenChar.ToString());
                    break;
            }

            return result;
        }
    }
}