namespace Featureban.Domain
{
    public class Token
    {
        public TokenType TokenType { get; }

        public Token(TokenType tokenType)
        {
            TokenType = tokenType;
        }
    }

    public enum TokenType
    {
        Eagle = 0,

        Tails = 1
    }
}