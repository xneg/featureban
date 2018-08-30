using System;

namespace Featureban.Domain
{
    public class WorkItemsFactory
    {
        private Scale _scale;
        public WorkItemsFactory(Scale scale)
        {
            _scale = scale;
        }

        public WorkItem CreateWorkItemFor(Player player)
        {
            return new WorkItem(player, _scale);
        }
    }
}