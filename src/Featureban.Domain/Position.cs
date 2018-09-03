using System;

namespace Featureban.Domain
{
    public class Position
    {
        private int _totalSteps;

        public PositionStatus Status { get; private set; }

        public int StepInProgress { get; private set; } = 0;

        public Position(int totalSteps)
        {
            _totalSteps = totalSteps;
        }

        public void StepUp()
        {
            switch (Status)
            {
                case PositionStatus.ToDo:
                    Status = PositionStatus.InProgress;
                    StepInProgress++;
                    break;
                case PositionStatus.InProgress:
                    if (StepInProgress++ >= _totalSteps)
                        Status = PositionStatus.Done;
                    break;
                case PositionStatus.Done:
                    throw new InvalidOperationException("You can not step up after done.");
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
    }
}