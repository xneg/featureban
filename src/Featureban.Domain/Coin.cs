using Featureban.Domain.Interfaces;
using System;

namespace Featureban.Domain
{
    public class Coin : ICoin
    {
        private readonly Random _random = new Random();

        public Token MakeToss()
        {
            var token = (_random.Next(2) > 0) ? Token.Eagle() : Token.Tails();
            return token;
        }
    }
}
