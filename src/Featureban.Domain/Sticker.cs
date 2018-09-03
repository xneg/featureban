using System;

namespace Featureban.Domain
{
    public class Sticker
    {
        public bool Blocked { get; private set; }

        private Position _position;

        public PositionStatus Status => _position.Status;

        public int StepInProgress => _position.StepInProgress;

        public Sticker(Scale scale)
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