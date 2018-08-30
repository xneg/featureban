using System;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class WorkItemTests
    {
        // ������
        // ID
        // ������ (������������/��������������)
        // �������� (�����)
        // ������� �� �����

        // �����
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
