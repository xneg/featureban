namespace Featureban.Domain
{
    public class Scale
    {
        private int _inProgressStepsCount;

        public Scale(int inProgressStepsCount)
        {
            _inProgressStepsCount = inProgressStepsCount;
        }

        public Position CreatePosition()
        {
            return new Position(_inProgressStepsCount);
        }
    }
}