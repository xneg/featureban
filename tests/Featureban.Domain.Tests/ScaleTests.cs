using Featureban.Domain.Positions;
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

            Assert.Equal(Position.ToDo(), position);
        }

        [Fact]
        public void WhenNewPositionStepIsInProgress()
        {
            var scale = new Scale(2);
            var position = scale.CreatePositionToDo();

            var newPosition = position.NextPosition();

            Assert.Equal(new PositionInProgress(0), newPosition);
        }

        [Fact]
        public void PositionCanBeDoneAfterStepUps()
        {
            var scale = new Scale(2);
            var position = scale.CreatePositionToDo();

            for (var i = 0; i < 3; i++)
                position = position.NextPosition();

            Assert.Equal(Position.Done(), position);
        }
    }
}
