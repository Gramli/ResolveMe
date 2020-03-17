namespace ResolveMe.MathCompiler.ExpressionTokens
{
    public class TextToken : IExpressionToken
    {
        public string Text { get; private set; }

        public TextToken()
        { 
        
        }
        public TextToken(string text)
        {
            this.Text = text;
        }

        public string GetStringRepresentation()
        {
            return this.Text;
        }

        public void Concat(TextToken other)
        {
            this.Text = $"{this.Text}{other.Text}";
        }
    }
}
