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
            var workItemsFactory = new WorkItemsFactory();

            var workItem = workItemsFactory.CreateWorkItemFor(player);

            Assert.Equal(player, workItem.Owner);
        }
    }
}
