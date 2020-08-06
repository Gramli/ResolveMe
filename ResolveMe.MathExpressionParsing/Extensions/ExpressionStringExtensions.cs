using System;
using System.Linq;

namespace ResolveMe.MathCompiler.Extensions
{
    public static class ExpressionStringExtensions
    {
        private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string numbers = "0123456789";
        public static string RemoveOuterBrackets(this string expression)
        {
            //check first and last chars
            if (MathDefinitions.leftRoundBracket == expression[0] && MathDefinitions.rightRoundBracket == expression[^1])
            {
                var innerValue = expression[1..^1];
                var bracketsCount = 0;

                //check inner expression
                for (var i = 0; i < innerValue.Length; i++)
                {
                    var isRightBracket = MathDefinitions.rightRoundBracket == innerValue[i];

                    if (bracketsCount == 0 && isRightBracket)
                    {
                        return expression;
                    }

                    if (isRightBracket)
                    {
                        bracketsCount--;
                    }
                    else if (MathDefinitions.leftRoundBracket == innerValue[i])
                    {
                        bracketsCount++;
                    }
                }
                return innerValue;
            }
            return expression;
        }
        public static string ReplaceInnerExpressionWithVariable(this string expression, string innerExpression, out string variable)
        {
            var generator = new Random();
            do
            {
                variable = GenerateVariable(2, generator);

            } while (expression.Contains(variable));

            return expression.Replace(innerExpression, variable);
        }

        public static bool IsLetter(this char item)
        {
            return alphabet.Contains(item);
        }

        public static bool IsNumber(this char item)
        {
            return numbers.Contains(item);
        }

        //make string extension maybe?
        public static string GenerateVariable(int length, Random generator)
        {
            var result = string.Empty;

            for (var i = 0; i < length; i++)
            {
                var index = generator.Next(0, alphabet.Length - 1);
                result += alphabet[index];
            }

            return result;
        }
    }
}
