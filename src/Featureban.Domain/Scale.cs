namespace Featureban.Domain
{
    public class Scale
    {
        public int _inProgressStepsCount { get; }

        public Scale(int inProgressStepsCount)
        {
            _inProgressStepsCount = inProgressStepsCount;
        }

        public bool IsValid(ProgressPosition progressPosition)
        {
            return progressPosition.Step <= _inProgressStepsCount;
        }
    }
}