using Featureban.Domain.Tests.DSL;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerHasInWorkItemsCreatedNewWorkItem()
        {
            var player = new Player();
            var workItem = Create.WorkItem().With(player).Please();

            Assert.Single(player.WorkItems, workItem);
        }
    }
}
