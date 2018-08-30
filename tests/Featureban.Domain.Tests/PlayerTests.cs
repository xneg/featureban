using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerHasInWorkItemsCreatedNewWorkItem()
        {
            var workItem = Create.WorkItem().Please();
            var player = Create.Player().Please();

            player.AddWorkItem(workItem);

            Assert.Single(player.WorkItems, workItem);
        }
    }
}
