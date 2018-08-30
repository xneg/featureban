using System;

namespace Featureban.Domain.Tests.DSL
{
    public class WorkItemBuilder
    {        
        private Scale _scale;
        private bool _blocked;

        public WorkItemBuilder()
        {
            _scale = new Scale(2);
        }
        

       public WorkItem Please()
        {
            var workItemsFactory = new WorkItemsFactory(_scale);
            var workItem = workItemsFactory.CreateWorkItem();
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