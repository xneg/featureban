using Xunit;

namespace Featureban.Domain.Tests
{
    public class ScaleTests
    {
        [Fact]
        public void CreateNewPositionAtStepToDo()
        {
            var scale = new Scale(2);

            var position = scale.CreatePositionToDo();

            Assert.Equal(PositionStatus.ToDo, position.Status);
        }

        [Fact]
        public void WhenNewPositionStepIsInProgress()
        {
            var scale = new Scale(2);
            var position = scale.CreatePositionToDo();

            var newPosition = position.NextPosition();

            Assert.Equal(PositionStatus.InProgress, newPosition.Status);
        }

        [Fact]
        public void PositionCanBeDoneAfterStepUps()
        {
            var scale = new Scale(2);
            var position = scale.CreatePositionToDo();

            for (var i = 0; i < 3; i++)
                position = position.NextPosition();

            Assert.Equal(PositionStatus.Done, position.Status);
        }
    }
}
