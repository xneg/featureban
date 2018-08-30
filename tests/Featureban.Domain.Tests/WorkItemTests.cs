using Featureban.Domain.Tests.DSL;
using System;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class WorkItemTests
    {
        // Листок
        // ID
        // Статус (заблокирован/незаблокирован)
        // Владелец (игрок)
        // Позиция на доске

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

            Assert.Equal(PositionStatus.ToDo, workItem.Position.Status);
        }
    }
}
