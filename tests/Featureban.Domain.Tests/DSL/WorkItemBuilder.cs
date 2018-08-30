namespace Featureban.Domain.Tests.DSL
{
    public class WorkItemBuilder
    {
        private Player _player;
        private Scale _scale;

        public WorkItemBuilder()
        {
            _player = new Player();
            _scale = new Scale(2);

        }

        public WorkItemBuilder With(Player player)
        {
            _player = player;
            return this;
        }

       public WorkItem Please()
        {
            var workItemsFactory = new WorkItemsFactory(_scale);
            return workItemsFactory.CreateWorkItemFor(_player);
        }
    }
}