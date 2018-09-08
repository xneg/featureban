using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerBoardBehaviorTests
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
            var stickersBoard = Create.StickersBoard(@"| InProgress (2) | InProgress (2) | Done |
                                                       | [P B]          |                | (0)  |").Please();

            var sticker = stickersBoard.GetStickersIn(ProgressPosition.First()).Single();

            stickersBoard.StepUp(sticker);

            AssertStickerBoard.Equal(@"| InProgress (2) | InProgress (2) | Done |
                                       | [P B]          |                | (0)  |",
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
        public void StickerInNextPosition_WhenStepUp()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [P  ]          |                | (0)  |").Please();
            var sticker = stickersBoard.GetStickersIn(ProgressPosition.First()).Single();

            stickersBoard.StepUp(sticker);

            AssertStickerBoard.Equal(@"| InProgress (1) | InProgress (1) | Done |
                                       |                |  [P ]          | (0)  |",
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
        public void NotStepUpSticker_WhenNextPositionIsFull()
        {
            var player = Create.Player().WithName("R").Please();
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | InProgress (1) | Done |
                                                       | [R  ]          | [P B]          | (0)  |")
                                                       .WithPlayer(player)
                                                       .Please();
            var sticker = stickersBoard.GetUnblockedStickerFor(player);

            stickersBoard.StepUp(sticker);

            AssertStickerBoard.Equal(@"| InProgress (1) | InProgress (1) | Done |
                                       | [R  ]          | [P B]          | (0)  |",
                                stickersBoard);
        }

        [Fact]
        public void StickerInProgressForEveryPlayer_WhenSetup()
        {
            var stickersBoard = Create.StickersBoard(@"| InProgress (1) | Done |
                                                       |                | (0)  |").Please();
            var player1 = Create.Player().WithName("P").Please();
            var player2 = Create.Player().WithName("R").Please();
            var players = new List<Player> { player1, player2 };

            stickersBoard.Setup(players);

            AssertStickerBoard.Equal(@"| InProgress (1) | Done |
                                       | [P  ]          | (0)  |
                                       | [R  ]          |      |",
                                stickersBoard);
        }
    }
}
