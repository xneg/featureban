using Featureban.Domain.Interfaces;

namespace Featureban.Domain.Tests.DSL
{
    internal class StubCoin : ICoin
    {
        private readonly Token _token;

        public StubCoin(Token token)
        {
            _token = token;
        }

        public Token MakeToss()
        {
            return _token;
        }
    }
}
