using System;

namespace Featureban.Domain
{
    public class WorkItem
    {
        public Player Owner { get; }
        public bool Blocked { get; private set; }
        public Position Position { get;}        

        public WorkItem(Player owner, Scale scale)
        {
            Owner = owner;
            Position = scale.CreatePosition();
        }

        public void Block()
        {
            Blocked = true;
        }
    }
}