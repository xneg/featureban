using System;

namespace Featureban.Domain
{
    public class WorkItem
    {
        public Player Owner { get; }
        public bool Blocked { get; private set; }
        private Position _position;
        public PositionStatus Status => _position.Status;

        public WorkItem(Player owner, Scale scale)
        {
            Owner = owner;
            _position = scale.CreatePosition();
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unblock()
        {
            Blocked = false;
        }

        public void StepUp()
        {
            _position.StepUp();
        }
    }
}