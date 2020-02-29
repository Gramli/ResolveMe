namespace ResolveMe.MathExpressionParsing
{
    /// <summary>
    /// Interface for matmematical expression grammars
    /// </summary>
    interface IMathExpressionGrammar
    {
        /// <summary>
        /// Determines that nonTerminal name is result expression if it belongs to grammar
        /// </summary>
        bool IsExpression(string nonTerminalName);

        bool IsNumber(string expression);

        bool IsVariable(string expression);

        bool IsPrefixFunction(string expression);
    }
}
