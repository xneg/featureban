using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerMovesTests
    {
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
