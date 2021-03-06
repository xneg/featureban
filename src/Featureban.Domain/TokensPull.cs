﻿namespace Featureban.Domain
{
    public class TokensPull
    {
        private int _tokensCount;

        public bool ContainsTokens => _tokensCount > 0;

        public void IncrementToken()
        {
            _tokensCount++;
        }

        public void DecrementToken()
        {
            _tokensCount--;
            _tokensCount = _tokensCount > 0 ? _tokensCount : 0;
        }

        public void EraseTokens()
        {
            _tokensCount = 0;
        }
    }
}