using System.Collections.Generic;

namespace Featureban.Domain
{
    public class Player
    {
        private List<WorkItem> _workItems = new List<WorkItem>();

        public IEnumerable<WorkItem> WorkItems => _workItems;

        public Player()
        {
        }

        public void AddWorkItem(WorkItem workItem)
        {
            _workItems.Add(workItem);
        }
    }
}