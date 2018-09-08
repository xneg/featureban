using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerBoardStrategyTests
    {
        [Fact]
        public void ReturnNull_WhenGetUnblockedSticker()
        {
            var player = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       | [P B]          | (0)  |")
                                                       .WithPlayer(player)
                                                       .Please();

            var sticker = stickersBoard.GetUnblockedStickerFor(player);

            Assert.Null(sticker);
        }

        [Fact]
        public void ReturnNullMoveableSticker_WhenStickerIsBlocked()
        {
            var player = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       | [P B]          | (0)  |")
                                                       .WithPlayer(player)
                                                       .Please();

            var sticker = stickersBoard.GetMoveableStickerFor(player);

            Assert.Null(sticker);
        }

        [Fact]
        public void ReturnNullMoveableSticker_WhenNextPositionIsFull()
        {
            var player = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [P B]          | [R  ]          | (0)  |")
                                                       .WithPlayer(player)
                                                       .Please();

            var sticker = stickersBoard.GetMoveableStickerFor(player);

            Assert.Null(sticker);
        }

        [Fact]
        public void ReturnNearestToDoneSticker_WhenGetBlockedSticker()
        {
            var player = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [P B]          | [P B]          | (0)  |")
                                                       .WithPlayer(player)
                                                       .Please();

            var sticker = stickersBoard.GetBlockedStickerFor(player);

            Assert.Equal(2, sticker.ProgressPosition.Step);
        }

        [Fact]
        public void ReturnNearestToDoneSticker_WhenGetMovableSticker()
        {
            var player = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [P  ]          | [P  ]          | (0)  |")
                                                       .WithPlayer(player)
                                                       .Please();

            var sticker = stickersBoard.GetMoveableStickerFor(player);

            Assert.Equal(2, sticker.ProgressPosition.Step);
        }

        [Fact]
        public void ReturnPlayerThatCanSpendToken_WhenHeHasMovableSticker()
        {
            var expectedPlayer = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [P  ]          |                | (0)  |")
                                                      .WithPlayer(expectedPlayer)
                                                      .Please();

            var player = stickersBoard.GetPlayerThatCanSpendToken();

            Assert.Equal(expectedPlayer, player);
        }

        [Fact]
        public void ReturnPlayerThatCanSpendToken_WhenHeHasBlockedSticker()
        {
            var expectedPlayer = Create.Player().WithName("P").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [P B]          |                | (0)  |")
                                                      .WithPlayer(expectedPlayer)
                                                      .Please();

            var player = stickersBoard.GetPlayerThatCanSpendToken();

            Assert.Equal(expectedPlayer, player);
        }
    }
}
