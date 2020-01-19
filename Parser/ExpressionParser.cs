using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    /// <summary>
    /// USE Shunting-yard_algorithm
    /// </summary>
    public class ExpressionParser
    {
        private List<Function> knownFuncts;

        public ExpressionParser(List<string> knownFuncts, char separator)
        {
            foreach(string item in knownFuncts)
            {
                this.knownFuncts.Add(new Function(item, separator));
            }
        }

        public ExpressionParser()
        {
            this.knownFuncts = new List<Function>();
        }

        public IParsedExpression Parse(string equation)
        {
            if (!ContainsFunction(equation)) throw new ArgumentException("Can't parse equation, can't find any function.");

            string clearEquation = RemoveOuterBrackets(equation);
            ParsedExpression root = new ParsedExpression(equation);

            

        }

        private string RemoveOuterBrackets(string equation)
        {
            string result = equation;
            if(equation[0].Equals('(') && equation[equation.Length-1].Equals(')'))
            {
                result = RemoveOuterBrackets(result.Substring(1, result.Length - 1));
            }
            return result;
        }

        public bool CheckCountOfBrackets(string equation)
        {
            for(int i=0;i<equation.Length;i++)
            {

            }
        }

        private bool ContainsFunction(string equation)
        {
            bool result = false;
            foreach(Function func in this.knownFuncts)
            {
                if (equation.Contains(func.Name))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
