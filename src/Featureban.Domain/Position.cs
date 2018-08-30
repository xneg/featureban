using System;

namespace Featureban.Domain
{
    public class Position
    {
        public PositionStatus Status { get; private set; }

        public void StepUp()
        {
            Status = PositionStatus.InProgress;
        }
    }
}