using Xunit;

namespace Featureban.Domain.Tests
{
    public class ScaleTests
    {
        [Fact]
        public void CreateNewPositionAtStepToDo()
        {
            var scale = new Scale();

            var position = scale.CreatePosition();

            Assert.Equal(PositionStatus.ToDo, position.Status);
        }

        [Fact]
        public void WhenNewPositionStepIsInProgress()
        {
            var scale = new Scale();
            var position = scale.CreatePosition();

            position.StepUp();

            Assert.Equal(PositionStatus.InProgress, position.Status);
        }
    }
}
