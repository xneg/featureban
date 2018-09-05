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
    }
}
