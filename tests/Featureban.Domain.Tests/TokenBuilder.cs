namespace Featureban.Domain.Tests.DSL
{
    public class TokenBuilder
    {
        TokenType tokenType;
        public TokenBuilder()
        {
            tokenType = TokenType.Tails;
        }
        public TokenBuilder Eagle()
        {
            tokenType = TokenType.Eagle;
            return this;
        }

        public Token Please()
        {
            return new Token(tokenType);
        }
    }
}