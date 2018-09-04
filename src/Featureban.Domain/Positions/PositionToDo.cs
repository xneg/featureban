namespace Featureban.Domain.Positions
{
    public sealed class PositionToDo : Position
    {
        public PositionToDo(int totalSteps) : base(totalSteps)
        {
        }

        public override Position NextPosition()
        {
            return new PositionInProgress(_totalSteps);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
