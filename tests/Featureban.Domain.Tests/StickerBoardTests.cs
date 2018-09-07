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
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();

            board.CreateStickerInProgress(player);

            Assert.NotEmpty(board.GetStickersIn(ProgressPosition.First()));
        }

        [Fact]
        public void StickerNotStepUp_WhenStickerBlocked()
        {
            var board = Create.StickersBoard().WithStickerInProgress().Please();
            var sticker = board.GetStickersIn(ProgressPosition.First()).Single();
            sticker.Block();

            board.StepUp(sticker);

            var firstPosition = ProgressPosition.First();
            var secondPosition = firstPosition.Next();
            Assert.Contains(sticker, board.GetStickersIn(firstPosition));
            Assert.DoesNotContain(sticker, board.GetStickersIn(secondPosition));
        }        

        [Fact]
        public void BoardReturnsUnblockedStickerForPlayer_WhenTakeStickerInWork()
        {
            var board = Create.StickersBoard().WithScale(1).Please();
            var player = Create.Player().Please();

            board.CreateStickerInProgress(player);

            Assert.NotNull(board.GetUnblockedStickerFor(player));
        }

        [Fact]
        public void BoardReturnsBlockedStickerForPlayer_WhenBlocked()
        {            
            var player = Create.Player().Please();
            var board = Create.StickersBoard().WithStickerInProgressFor(player).Please();
            var sticker = board.GetUnblockedStickerFor(player);

            sticker.Block();             

            Assert.NotNull(board.GetBlockedStickerFor(player));
        }

        [Fact]
        public void BoardCanNotAddStickerToWork_WhenWipIsReached()
        {
            var board = Create.StickersBoard().WithWip(1).Please();
            var player = Create.Player().Please();

            board.CreateStickerInProgress(player);

            Assert.False(board.CanCreateStickerInProgress());
        }

        [Fact]
        public void BoardReturnsMoveableStickerForPlayer()
        {
            var board = Create.StickersBoard().WithScale(1).Please();
            var player = Create.Player().Please();

            board.CreateStickerInProgress(player);

            Assert.NotNull(board.GetMoveableStickerFor(player));
        }

        [Fact]
        public void InDoneStickersIncrement_WhenStickerIsDone()
        {
            var board = Create.StickersBoard().WithScale(1).WithStickerInProgress().Please();
            var sticker = board.GetStickersIn(ProgressPosition.First()).Single();

            board.StepUp(sticker);

            Assert.Equal(1, board.DoneStickers);
        }

        [Fact]
        public void ReturnNull_WhenGetUnblockedSticker()
        {
            var player = Create.Player().Please();
            var stickersBoard = Create.StickersBoard().Please();

            var sticker = stickersBoard.GetUnblockedStickerFor(player);

            Assert.Null(sticker);
        }


        [Fact]
        public void NotStepUpSticker_WhenNextPositionIsFull()
        {
            var player = Create.Player().Please();
            var stickersBoard = Create.StickersBoard()
                .WithScale(2).And()
                .WithWip(1).And()
                .WithStickerInProgressForPosition(2.Position())                
                .Please();
            var sticker = stickersBoard.CreateStickerInProgress(player);

            stickersBoard.StepUp(sticker);

            Assert.Equal(sticker, stickersBoard.GetStickersIn(ProgressPosition.First()).Single());
        }

        [Fact]
        public void OneStickerInProgress_WhenSetupForOnePlayer()
        {
            var player = Create.Player().Please();
            var players = new List<Player>();
            players.Add(player);
            var stickersBoard = Create.StickersBoard().Please();

            stickersBoard.Setup(players);

            Assert.Single(stickersBoard.GetStickersIn(ProgressPosition.First()));
        }


        [Fact]
        public void ReturnPlayerWichCanSpendToken_WhenHimHaveMovableSticker()
        {
            var expectedPlayer = Create.Player().Please();
            var stickerBoard = Create.StickersBoard().WithStickerInProgressFor(expectedPlayer).Please();

            var player = stickerBoard.GetPlayerWichCanSpendToken();

            Assert.Equal(expectedPlayer, player);
        }

        [Fact]
        public void ReturnPlayerWichCanSpendToken_WhenHimHaveBlockedSticker()
        {
            var expectedPlayer = Create.Player().Please();
            var stickerBoard = Create.StickersBoard().WithBlockedStickerInProgressFor(expectedPlayer).Please();

            var player = stickerBoard.GetPlayerWichCanSpendToken();

            Assert.Equal(expectedPlayer, player);
        }

        [Fact]
        public void StickerCanMove_WhenWipIsNull()
        {
            var stickerBoard = Create.StickersBoard().Please();

            Assert.True(stickerBoard.CanMoveTo(ProgressPosition.First()));
        }

        [Fact]
        public void StickerCanMove_WhenNewxtPositionIsNotFull()
        {
            var stickerBoard = Create.StickersBoard().WithWip(1).Please();

            Assert.True(stickerBoard.CanMoveTo(ProgressPosition.First()));
        }

        [Fact]
        public void StickerCanNotMove_WhenNewxtPositionIsNotFull()
        {
            var stickerBoard = Create.StickersBoard().WithWip(1).WithStickerInProgress().Please();

            Assert.False(stickerBoard.CanMoveTo(ProgressPosition.First()));
        }


    }
}
