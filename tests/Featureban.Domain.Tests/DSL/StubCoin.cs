using Featureban.Domain.Interfaces;

namespace Featureban.Domain.Tests.DSL
{
    public class StubCoin : ICoin
    {
        private TokenType _tokenType;

        public StubCoin(TokenType tokenType)
        {
            _tokenType = tokenType;
        }

        public Token MakeToss()
        {
            return new Token(_tokenType);
        }
    }
}
