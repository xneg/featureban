using System;

namespace Featureban.Domain
{
    public class WorkItemsFactory
    {
        public WorkItemsFactory()
        {
        }

        public WorkItem CreateWorkItemFor(Player player)
        {
            return new WorkItem(player);
        }
    }
}