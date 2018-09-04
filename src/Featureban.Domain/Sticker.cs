using System;
using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class Sticker
    {
        public bool Blocked { get; private set; }

        private Position _position;

        public PositionStatus Status
        {
            get
            {
                switch (_position)
                {
                    case PositionDone _:
                        return PositionStatus.Done;
                    case PositionInProgress _:
                        return PositionStatus.InProgress;
                    case PositionToDo _:
                        return PositionStatus.ToDo;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public int StepInProgress
        {
            get
            {
                switch (_position)
                {
                    case PositionToDo _:
                    case PositionDone _:
                        return 0;
                    case PositionInProgress positionInProgress:
                        return positionInProgress.CurrentStep;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public Sticker(Scale scale)
        {
            _position = scale.CreatePositionToDo();
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
                _position = _position.NextPosition();
            }
        }
    }
}