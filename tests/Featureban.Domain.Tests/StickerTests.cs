using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class StickerTests
    {
        // Листок
        // ID

        // Доска
        // 

        [Fact]
        public void NewStickerNotBlocked()
        {
            var sticker = Create.Sticker().Please();

            Assert.False(sticker.Blocked);
        }

        [Fact]
        public void NewStickerHasPositionToDo()
        {
            var sticker = Create.Sticker().Please();

            Assert.Equal(PositionStatus.ToDo, sticker.Status);
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
        public void StickerStatusIsInProgress_WhenStepUp()
        {
            var sticker = Create.Sticker().Please();

            sticker.StepUp();

            Assert.Equal(PositionStatus.InProgress, sticker.Status);
        }

        [Fact]
        public void StickerStatusToDo_WhenStepUpAndBlocked()
        {
            var sticker = Create.Sticker().Blocked().Please();

            sticker.StepUp();

            Assert.Equal(PositionStatus.ToDo, sticker.Status);
        }
    }
}
