using Featureban.Domain.Tests.DSL;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Featureban.Domain.Tests
{
  public  class TokensPullTests
    {
        [Fact]
        public void LoseToken_WhenDecrement()
        {
            var tokensPull = Create.TokenPull().With(1.Token()).Please();

            tokensPull.DecrementToken();

            Assert.False(tokensPull.ContainsTokens);
        }
        [Fact]
        public void DoNothing_WhenDecrementWithoutTokens()
        {
            var tokensPull = Create.TokenPull().Please();

            tokensPull.DecrementToken();

            Assert.False(tokensPull.ContainsTokens);
        }
    }
}
