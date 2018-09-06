namespace Featureban.Domain
{
    public class Scale
    {
        private readonly int _inProgressStepsCount;

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