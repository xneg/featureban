namespace Featureban.Domain
{
    public class Scale
    {
        private readonly int _inProgressStepsCount;

        public Scale(int inProgressStepsCount)
        {
            _inProgressStepsCount = inProgressStepsCount;
        }

        public Position CreatePositionToDo()
        {
            return Position.ToDo(_inProgressStepsCount);
        }
    }
}