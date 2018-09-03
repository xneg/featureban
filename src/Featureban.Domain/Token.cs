namespace Featureban.Domain
{
    public class Token
    {
       TokenType TokenType { get; }

        public Token(TokenType tokenType)
        {
            TokenType = tokenType;
        }
    }

    public  enum TokenType
    {
        Eagle = 0,
        Tails = 1
    }
}