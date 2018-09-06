using Featureban.Domain.Interfaces;
using System;

namespace Featureban.Domain
{
    public class Coin : ICoin
    {
        private static readonly Random _random = new Random();

        public Token MakeToss()
        {
            var tokenType = (_random.Next(2) > 0) ? TokenType.Eagle : TokenType.Tails;
            return new Token(tokenType);
        }
    }
}
