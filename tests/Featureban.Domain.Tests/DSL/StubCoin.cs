using Featureban.Domain.Interfaces;

namespace Featureban.Domain.Tests.DSL
{
    internal class StubCoin : ICoin
    {
        private readonly TokenType _tokenType;

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
