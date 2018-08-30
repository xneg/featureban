namespace Featureban.Domain
{
    public class WorkItemsFactory
    {
        private Scale _scale;

        public WorkItemsFactory(Scale scale)
        {
            _scale = scale;
        }

        public WorkItem CreateWorkItem()
        {
            var workItem = new WorkItem(_scale);
            return workItem;
        }
    }
}