namespace Featureban.Domain.Tests.DSL
{
    public class TokenBuilder
    {
        private TokenType tokenType;

        public TokenBuilder()
        {
            tokenType = TokenType.Tails;
        }

        public TokenBuilder Eagle()
        {
            tokenType = TokenType.Eagle;
            return this;
        }

        public TokenBuilder Tails()
        {
            tokenType = TokenType.Tails;
            return this;
        }

        public Token Please()
        {
            return new Token(tokenType);
        }
    }
}