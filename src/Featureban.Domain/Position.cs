using System;

namespace Featureban.Domain
{
    public class Position
    {
        private int _currentStep = 0;
        private int _totalSteps;

        public PositionStatus Status { get; private set; }

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
                    break;
                case PositionStatus.InProgress:
                    if (++_currentStep >= _totalSteps)
                        Status = PositionStatus.Done;
                    break;
                case PositionStatus.Done:
                    throw new InvalidOperationException("You can not step up after done.");
            }
        }
    }
}