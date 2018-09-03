using Featureban.Domain.Tests.DSL;
using System;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerHasSticker_WhenAddSticker()
        {
            var sticker = Create.Sticker().Please();
            var player = Create.Player().Please();

            player.AddSticker(sticker);

            Assert.Single(player.Stickers, sticker);
        }

        [Fact]
        public void PlayerCanGetToken_WhenMakeToss()
        {
            var player = Create.Player().Please();

            player.MakeToss();

            Assert.Equal(1, player.Tokens.Count);
        }

        [Fact]
        public void PlayerWithTokenLoseToken_WhenSpendToken()
        {
            var player = Create.Player().WithTokens(1.Token()).Please();

            player.SpendToken();

            Assert.Equal(0, player.Tokens.Count);
        }

        [Fact]
        public void PlayerCanNotSpent_WhenHasEagleToken()
        {
            var eagle = Create.Token().Eagle().Please();
            var player = Create.Player().WithToken(eagle).Please();
            var player2 = Create.Player().Please();

            Assert.Throws<InvalidOperationException>(() => player.GiveTokenTo(player2));
        }
    }
}
