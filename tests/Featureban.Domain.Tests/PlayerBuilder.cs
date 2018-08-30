namespace Featureban.Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private WorkItem _workItem;
        
        public PlayerBuilder With(WorkItem workItem)
        {
            _workItem = workItem;
            return this;
        }

        public Player Please()
        {
            var player = new Player();
            if (_workItem != null)
            {
                player.AddWorkItem(_workItem);
            }
            return player;
        }
    }
}