using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveMe.MathCompiler.Extensions
{
    public static class ExpressionStringExtensions
    {
        public static string RemoveOuterBrackets(this string expression)
        {
            //TODO DAN IMPLEMENT
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

        //make string extension maybe?
        public static string GenerateVariable(int length, Random generator)
        {
            var result = string.Empty;

            var charArray = "abcdefghijklmnopqrstuvwxyz";
            for (var i = 0; i < length; i++)
            {
                var index = generator.Next(0, charArray.Length - 1);
                result += charArray[index];
            }

            return result;
        }
    }
}
