namespace Featureban.Domain
{
    public class WorkItem
    {
        public Player Owner { get; }
        public bool Blocked { get; set; }

        public WorkItem(Player owner)
        {
            Owner = owner;
        }
    }
}