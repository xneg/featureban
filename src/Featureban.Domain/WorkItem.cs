namespace Featureban.Domain
{
    public class WorkItem
    {
        public Player Owner { get; }

        public WorkItem(Player owner)
        {
            Owner = owner;
        }
    }
}