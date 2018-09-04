using System;

namespace Featureban.Domain
{
    public class Position
    {
        private readonly int _totalSteps;

        public PositionStatus Status { get; }

        public int StepInProgress { get; }

        public Position(int totalSteps, PositionStatus status, int stepInProgress)
        {
            _totalSteps = totalSteps;
            StepInProgress = stepInProgress;
            Status = status;
        }

        public Position NextPosition()
        {
            switch (Status)
            {
                case PositionStatus.ToDo:
                    return new Position(_totalSteps, PositionStatus.InProgress, 1);
                case PositionStatus.InProgress:
                    return (StepInProgress == _totalSteps)
                        ? Position.Done()
                        : new Position(_totalSteps, PositionStatus.InProgress, StepInProgress + 1);
                case PositionStatus.Done:
                    throw new InvalidOperationException("You can not step up after done.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Position;
            return (this == other);
        }

        public override int GetHashCode()
        {
            return (int)Status ^ (StepInProgress + 1);
        }

        public static bool operator == (Position p1, Position p2)
        {
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            if (ReferenceEquals(p1, null))
            {
                return false;
            }

            if (ReferenceEquals(p2, null))
            {
                return false;
            }

            return (p1.Status == p2.Status && p1.StepInProgress == p2.StepInProgress);
        }

        public static bool operator != (Position p1, Position p2)
        {
            return p1 != p2;
        }

        public static Position ToDo(int totalSteps)
        {
            return new Position(totalSteps, PositionStatus.ToDo, 0);
        }

        public static Position Done()
        {
            return new Position(0, PositionStatus.Done, 0);
        }
    }
}