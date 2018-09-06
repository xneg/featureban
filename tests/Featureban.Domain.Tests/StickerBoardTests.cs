using System.Linq;
using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerBoardTests
    {
        [Fact]
        public void StickerInStatusToDo_WhenCreated()
        {
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();

            var sticker = board.CreateStickerFor(player);

            Assert.Equal(StickerStatus.Todo, sticker.Status);
        }

        [Fact]
        public void StickerIsInProgress_WhenTakeInWork()
        {
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();

            board.TakeStickerInWorkFor(player);

            var sticker = board.GetStickersIn(PositionNew.First()).Single();

            Assert.Equal(StickerStatus.InProgress, sticker.Status);
        }

        [Fact]
        public void StickerNotStepUp_WhenStickerBlocked()
        {
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();
            board.TakeStickerInWorkFor(player);

            var sticker = board.GetStickersIn(PositionNew.First()).Single();
            sticker.Block();

            board.StepUp(sticker);

            var firstPosition = PositionNew.First();
            var secondPosition = firstPosition.Next();

            Assert.Contains(sticker, board.GetStickersIn(firstPosition));
            Assert.DoesNotContain(sticker, board.GetStickersIn(secondPosition));
        }

        [Fact]
        public void StickerInDone_WhenStepUps()
        {
            var board = Create.StickersBoard().WithScale(1).Please();
            var player = Create.Player().Please();
            board.TakeStickerInWorkFor(player);

            var sticker = board.GetStickersIn(PositionNew.First()).Single();

            board.StepUp(sticker);
            board.StepUp(sticker);

            Assert.Equal(StickerStatus.Done, sticker.Status);            
        }

        [Fact]
        public void BoardReturnsUnblockedStickerForPlayer_WhenTakeStickerInWork()
        {
            var board = Create.StickersBoard().WithScale(1).Please();
            var player = Create.Player().Please();

            board.TakeStickerInWorkFor(player);

            Assert.NotNull(board.GetUnblockedStickerFor(player));
        }

        [Fact]
        public void BoardReturnsBlockedStickerForPlayer_WhenBlocked()
        {
            var board = Create.StickersBoard().WithScale(1).Please();
            var player = Create.Player().Please();
            board.TakeStickerInWorkFor(player);
            var sticker = board.GetUnblockedStickerFor(player);
            sticker.Block();             

            Assert.NotNull(board.GetBlockedStickerFor(player));
        }

        [Fact]
        public void BoardCanNotAddStickerToWork_WhenWipIsReached()
        {
            var board = Create.StickersBoard().WithScale(1).WithWip(1).Please();
            var player = Create.Player().Please();
            var player2 = Create.Player().Please();
            board.TakeStickerInWorkFor(player);

            board.TakeStickerInWorkFor(player2);

            Assert.Null(board.GetUnblockedStickerFor(player2));
        }
    }
}
