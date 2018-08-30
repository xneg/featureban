using Xunit;

namespace Featureban.Domain.Tests
{
    public class ScaleTests
    {
        [Fact]
        public void CreateNewPositionAtStepToDo()
        {
            var scale = new Scale(2);

            var position = scale.CreatePosition();

            Assert.Equal(PositionStatus.ToDo, position.Status);
        }

        [Fact]
        public void WhenNewPositionStepIsInProgress()
        {
            var scale = new Scale(2);
            var position = scale.CreatePosition();

            position.StepUp();

            Assert.Equal(PositionStatus.InProgress, position.Status);
        }

        [Fact]
        public void PositionCanBeDoneAfterStepUps()
        {
            var scale = new Scale(2);
            var position = scale.CreatePosition();

            for (var i = 0; i < 3; i++)
                position.StepUp();

            Assert.Equal(PositionStatus.Done, position.Status);
        }
    }
}
