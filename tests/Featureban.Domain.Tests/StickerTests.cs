using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerTests
    {
        [Fact]
        public void NewStickerNotBlocked()
        {
            var sticker = Create.Sticker().Please();

            Assert.False(sticker.Blocked);
        }

        [Fact]
        public void IfBlockStickerItWillBeBlocked()
        {
            var sticker = Create.Sticker().Please();

            sticker.Block();

            Assert.True(sticker.Blocked);
        }

        [Fact]
        public void IfUnblockStickerItWillBeNotBlocked()
        {
            var sticker = Create.Sticker().Blocked().Please();

            sticker.Unblock();

            Assert.False(sticker.Blocked);
        }

        [Fact]
        public void StickerBoardCreateStickerForPlayer()
        {
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();

            var sticker = board.CreateStickerFor(player);

            Assert.Equal(player, sticker.Owner);
        }
    }
}
