using System;

namespace Featureban.Domain
{
    public class WorkItem
    {
        public bool Blocked { get; private set; }

        private Position _position;

        public PositionStatus Status => _position.Status;

        public WorkItem(Scale scale)
        {
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
            if (!Blocked)
            {
                _position.StepUp();
            }
        }
    }
}