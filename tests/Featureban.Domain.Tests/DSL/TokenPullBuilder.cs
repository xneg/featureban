namespace Featureban.Domain.Tests.DSL
{
    public class TokensPullBuilder
    {
        private int _tokensCount;
        
        public TokensPullBuilder With(int tokenCount)
        {
            _tokensCount = tokenCount;
            return this;
        }

        public TokensPull Please()
        {
            var tokensPull = new TokensPull();

            for(var i = 0; i< _tokensCount; i++)
            {
                tokensPull.IncrementToken();
            }

            return tokensPull;
        }
    }
}