using System;
using System.Collections.Generic;
using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class Sticker
    {
        public bool Blocked { get; private set; }

        private Position _position;

        private Player _owner;

        public Player Owner => _owner;

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

        public Sticker(Scale scale, Player player)
        {
            _position = scale.CreatePositionToDo();
            _owner = player;
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