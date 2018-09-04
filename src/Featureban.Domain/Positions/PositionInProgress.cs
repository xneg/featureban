namespace Featureban.Domain.Positions
{
    public sealed class PositionInProgress : Position
    {
        public int CurrentStep { get; } = 1;

        public PositionInProgress(int totalSteps) : base(totalSteps)
        {
        }

        protected PositionInProgress(int totalSteps, int currentStep) : this(totalSteps)
        {
            CurrentStep = currentStep;
        }

        public override Position NextPosition()
        {
            if (CurrentStep != _totalSteps)
                return new PositionInProgress(_totalSteps, CurrentStep + 1);
            else
                return new PositionDone();
        }

        public override bool Equals(object obj)
        {
            var other = obj as PositionInProgress;

            if ((object)other == null)
                return false;

            return CurrentStep == other.CurrentStep;
        }

        public override int GetHashCode()
        {
            return (CurrentStep + 1) ^ 2;
        }
    }
}
