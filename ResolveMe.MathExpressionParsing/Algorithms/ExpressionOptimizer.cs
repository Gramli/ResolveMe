using System.Collections.Generic;
using System.Text;
using ResolveMe.MathCompiler.ExpressionTokens;

namespace ResolveMe.MathCompiler.Algorithms
{
    public class ExpressionOptimizer
    {
        private readonly char _leftBracket = '(';
        private readonly char _rightBracket = ')';
        private readonly uint _optimalExpressionLength;
        //private readonly HashSet<char> _operators = new HashSet<char>() { '*', '/', '+', '-', '^' };

        public ExpressionOptimizer(uint optimalExpressionLength)
        {
            this._optimalExpressionLength = optimalExpressionLength;
        }

        public IEnumerable<IExpressionToken> SplitLongExpression(string value)
        {
            if (value.Length > this._optimalExpressionLength && !string.IsNullOrEmpty(value))
            {
                var editedValue = TryRemoveOuterBrackets(value);
                var expressions = new List<IExpressionToken>();

                foreach (var token in editedValue)
                {
                    if (!(token is RawToken))
                    {
                        expressions.Add(token);
                    }
                    else
                    {
                        expressions.AddRange(GetTokenTokens(token));
                    }
                }

                return expressions;
            }

            return new List<IExpressionToken>(1) { new RawToken(value) };
        }

        private IEnumerable<IExpressionToken> GetTokenTokens(IExpressionToken token)
        {
            var tokenStringValue = token.GetStringRepresentation();

            var expressions = new List<IExpressionToken>();
            var brackets = 0;

            for (var i = 0; i < tokenStringValue.Length; i++)
            {
                if (tokenStringValue[i].Equals(_leftBracket))
                {
                    brackets++;
                }
                else if (tokenStringValue[i].Equals(_rightBracket))
                {
                    brackets--;
                }

                if (MathDefinitions.OperatorDefinitions.ContainsKey(tokenStringValue[i]) && brackets == 0 && i != 0)
                {
                    expressions.AddRange(SplitLongExpression(tokenStringValue[..i]));
                    expressions.Add(CreateOperatorToken(tokenStringValue[i]));
                    expressions.AddRange(SplitLongExpression(tokenStringValue[(i + 1)..]));
                    break;

                }

                if (tokenStringValue[i].Equals(','))
                {
                    //TODO DAN zase pocitat pocet zavorek a pokud jsem na 0 a je leva ukonci nebo carka
                    var stringhBuilder = new StringBuilder();
                    //pridej token pred carkou, ukoncujici levou zavorkou
                    for (var j = i - 1; j >= 0; j--)
                    {
                        if (tokenStringValue[j].Equals(_leftBracket)) break;
                        stringhBuilder.Insert(0,tokenStringValue[j]);
                    }
                    expressions.AddRange(SplitLongExpression(stringhBuilder.ToString()));
                    expressions.Add(new CommaToken());

                    //TODO DAN tohle neni potreba, na zbytek proste zavolam rekurzi
                    stringhBuilder.Clear();
                    for (; i < tokenStringValue.Length; i++)
                    {
                        if (tokenStringValue[i].Equals(_rightBracket)) break;
                        stringhBuilder.Append(tokenStringValue[i]);
                    }

                    expressions.AddRange(SplitLongExpression(stringhBuilder.ToString()));
                    //pokracuj dopredu po dalsi carku nebo pravou zavorku
                }
            }

            if (expressions.Count == 0)
            {
                expressions.Add(token);
            }

            return expressions;

        }

        public IEnumerable<IExpressionToken> TryRemoveOuterBrackets(string value)
        {
            var result = new List<IExpressionToken>();

            if (value[0].Equals(_leftBracket) && value[^1].Equals(_rightBracket))
            {
                var innerValue = value[1..^1];
                var brackets = 0;

                for (var i = 0; i < innerValue.Length; i++)
                {
                    var equalsRightBracket = innerValue[i].Equals(_rightBracket);

                    if (brackets.Equals(0) && equalsRightBracket)
                    {
                        result.Add(new RawToken(value));
                        return result;
                    }
                    else if (innerValue[i].Equals(_leftBracket))
                    {
                        brackets++;
                    }
                    else if (equalsRightBracket)
                    {
                        brackets--;
                    }
                }

                result.Add(new LeftBracketToken());
                result.Add(new RawToken(innerValue));
                result.Add(new RightBracketToken());
                return result;
            }

            result.Add(new RawToken(value));
            return result;
        }

        private OperatorToken CreateOperatorToken(char token)
        {
            var (precedence, operatorAssociativity) = MathDefinitions.OperatorDefinitions[token];
            return new OperatorToken(token, precedence, operatorAssociativity);

        }
    }
}
