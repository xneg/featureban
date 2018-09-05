using Featureban.Domain.Positions;
using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerBoardTests
    {
        [Fact]
        public void StickerInPositionToDo_WhenCreated()
        {
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();

            var sticker = board.CreateStickerFor(player);

            Assert.Contains(sticker, board.GetStickersIn(Position.ToDo()));
        }

        [Fact]
        public void StickerIsInProgress_WhenStepUpFromToDo()
        {
            var board = Create.StickersBoard().Please();
            var player = Create.Player().Please();
            var sticker = board.CreateStickerFor(player);

            board.StepUp(sticker);

            Assert.DoesNotContain(sticker, board.GetStickersIn(Position.ToDo()));
            Assert.Contains(sticker, board.GetStickersIn(new PositionInProgress(1)));
        }
    }
}
