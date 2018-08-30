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
            var workItem = new WorkItem(_scale);
            player.AddWorkItem(workItem);

            return workItem;
        }
    }
}