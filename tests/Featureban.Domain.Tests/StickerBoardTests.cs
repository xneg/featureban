using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerBoardTests
    {       

        [Fact]
        public void StickerIsInProgress_WhenTakeInWork()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (2) | Done |
                                                       |                | (0)  |").Please();

            var player = Create.Player().WithName("P").Please();

            stickersBoard.CreateStickerInProgress(player);

            AssertStickerBoard.Equal(@"| InProgress (2) | Done |
                                       | [P  ]          | (0)  |",
                                stickersBoard);
        }
        
        [Fact]
        public void StickerNotStepUp_WhenStickerBlocked()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (2) | Done |
                                                       | [P B]          | (0)  |").Please();

            var sticker = stickersBoard.GetStickersIn(ProgressPosition.First()).Single();

            stickersBoard.StepUp(sticker);

            AssertStickerBoard.Equal(@"| InProgress (2) | Done |
                                       | [P B]          | (0)  |",
                               stickersBoard);
        }        

        [Fact]
        public void BoardReturnsUnblockedStickerForPlayer_WhenTakeStickerInWork()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (2) | Done |
                                                       |                | (0)  |").Please();

            var player = Create.Player().WithName("P").Please();

            stickersBoard.CreateStickerInProgress(player);

            AssertStickerBoard.Equal(@"| InProgress (2) | Done |
                                       | [P  ]          | (0)  |",
                                stickersBoard);
        }

        [Fact]
        public void BoardReturnsBlockedStickerForPlayer_WhenBlocked()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (2) | Done |
                                                       | [P  ]          | (0)  |").Please();

            var sticker = stickersBoard.GetStickersIn(ProgressPosition.First()).Single();

            sticker.Block();

            AssertStickerBoard.Equal(@"| InProgress (2) | Done |
                                       | [P B]          | (0)  |",
                               stickersBoard);            
        }

        [Fact]
        public void BoardCanNotAddStickerToWork_WhenWipIsReached()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                      | [P  ]          | (0)  |").Please();

            var player = Create.Player().WithName("P").Please();

            stickersBoard.CreateStickerInProgress(player);

            AssertStickerBoard.Equal(@"| InProgress (1) | Done |
                                       | [P  ]          | (0)  |",
                                stickersBoard);
        }        

        [Fact]
        public void InDoneStickersIncrement_WhenStickerIsDone()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       | [P  ]          | (0)  |").Please();
            var sticker = stickersBoard.GetStickersIn(ProgressPosition.First()).Single();

            stickersBoard.StepUp(sticker);

            AssertStickerBoard.Equal(@"| InProgress (1) | Done |
                                       |                | (1)  |",
                                stickersBoard);
        }

        [Fact]
        public void BoardReturnsMoveableStickerForPlayer()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       |                | (0)  |").Please();
            var player = Create.Player().WithName("P").Please();

            stickersBoard.CreateStickerInProgress(player);

            AssertStickerBoard.Equal(@"| InProgress (1) | Done |
                                       | [P  ]          | (0)  |",
                                stickersBoard);
        }

        [Fact]
        public void ReturnNull_WhenGetUnblockedSticker()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       | [P B]          | (0)  |").Please();
            var player = Create.Player().WithName("P").Please();

            var sticker = stickersBoard.GetUnblockedStickerFor(player);

            Assert.Null(sticker);
        }

        [Fact]
        public void NotStepUpSticker_WhenNextPositionIsFull()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [R  ]          | [P B]          | (0)  |").Please();
            var player = Create.Player().WithName("R").Please();

            var sticker = stickersBoard.GetUnblockedStickerFor(player);

            stickersBoard.StepUp(sticker);

            AssertStickerBoard.Equal(@"| InProgress (1) | InProgress (1) | Done |
                                       | [R  ]          | [P B]          | (0)  |",
                                stickersBoard);
        }

        [Fact]
        public void OneStickerInProgress_WhenSetupForOnePlayer()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       |                | (0)  |").Please();
            var player = Create.Player().WithName("P").Please();
            var players = new List<Player> {player};

            stickersBoard.Setup(players);

            AssertStickerBoard.Equal(@"| InProgress (1) | Done |
                                       | [P  ]          | (0)  |",
                                stickersBoard);
        }

        [Fact]
        public void ReturnPlayerThatCanSpendToken_WhenHeHasMovableSticker()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                      | [P  ]          | (0)  |").Please();
            var expectedPlayer = Create.Player().WithName("P").Please();

            var player = stickersBoard.GetPlayerThatCanSpendToken();

            Assert.Equal(expectedPlayer, player);
        }

        [Fact]
        public void ReturnPlayerThatCanSpendToken_WhenHeHasBlockedSticker()
        {
           var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                      | [P B]          | (0)  |").Please();
            var expectedPlayer = Create.Player().WithName("P").Please();

            var player = stickersBoard.GetPlayerThatCanSpendToken();

            Assert.Equal(expectedPlayer, player);
        }

        [Fact]
        public void StickerCanMove_WhenWipIsNull()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress     | Done |
                                                       |                | (0)  |").Please();

            Assert.True(stickersBoard.CanMoveTo(ProgressPosition.First()));
        }

        [Fact]
        public void StickerCanMove_WhenNextPositionIsNotFull()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       |                | (0)  |").Please();

            Assert.True(stickersBoard.CanMoveTo(ProgressPosition.First()));
        }

        [Fact]
        public void StickerCanNotMove_WhenNextPositionIsFull()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                      | [P  ]          | (0)  |").Please();

            Assert.False(stickersBoard.CanMoveTo(ProgressPosition.First()));
        }
    }
}
