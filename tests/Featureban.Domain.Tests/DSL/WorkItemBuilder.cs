using System;

namespace Featureban.Domain.Tests.DSL
{
    public class WorkItemBuilder
    {
        private Player _player;
        private Scale _scale;
        private bool _blocked;

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
            var workItem = workItemsFactory.CreateWorkItemFor(_player);
            if(_blocked)
            {
                workItem.Block();
            }
            return workItem;
        }

        public WorkItemBuilder Blocked()
        {
            _blocked = true;
            return this;
        }
    }
}