namespace ResolveMe.MathCompiler
{
    public class ExpressionOptimizer
    {
        private readonly char leftBracket = '(';
        private readonly char rightBracket = ')';

        public string TryRemoveOuterBrackets(string value)
        {
            if (value[0].Equals(leftBracket) && value[value.Length - 1].Equals(rightBracket))
            {
                string innerValue = value[1..(value.Length - 1)];
                var brackets = 0;

                for (int i = 0; i < innerValue.Length; i++)
                {
                    bool equalsRightBracket = innerValue[i].Equals(rightBracket);

                    if (brackets.Equals(0) && equalsRightBracket)
                    {
                        return value;
                    }
                    else if (innerValue[i].Equals(leftBracket))
                    {
                        brackets++;
                    }
                    else if (equalsRightBracket)
                    {
                        brackets--;
                    }
                }

                return innerValue;
            }

            return value;
        }
    }
}
