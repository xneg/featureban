using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class WorkItemTests
    {
        // Листок
        // ID

        // Доска
        // 

        [Fact]
        public void FactoryCreateWorkItemForPlayer()
        {
            var player = new Player();
            var workItem = Create.WorkItem().With(player).Please();

            Assert.Equal(player, workItem.Owner);
        }

        [Fact]
        public void NewWorkItemNotBlocked()
        {
            var workItem = Create.WorkItem().Please();

            Assert.False(workItem.Blocked);
        }

        [Fact]
        public void NewWorkItemHasPositionToDo()
        {
            var workItem = Create.WorkItem().Please();

            Assert.Equal(PositionStatus.ToDo, workItem.Status);
        }

        [Fact]
        public void IfBlockWorkItemItWillBeBlocked()
        {
            var workItem = Create.WorkItem().Please();

            workItem.Block();

            Assert.True(workItem.Blocked);
        }

        [Fact]
        public void IfUnblockWorkItemItWillBeNotBlocked()
        {
            var workItem = Create.WorkItem().Blocked().Please();

            workItem.Unblock();

            Assert.False(workItem.Blocked);
        }

        [Fact]
        public void WorkItemStatusIsInProgress_WhenStepUp()
        {
            var workItem = Create.WorkItem().Please();

            workItem.StepUp();

            Assert.Equal(PositionStatus.InProgress, workItem.Status);
        }

        [Fact]
        public void WorkItemStatusToDo_WhenStepUpAndBlocked()
        {
            var workItem = Create.WorkItem().Blocked().Please();

            workItem.StepUp();

            Assert.Equal(PositionStatus.ToDo, workItem.Status);
        }
    }
}
