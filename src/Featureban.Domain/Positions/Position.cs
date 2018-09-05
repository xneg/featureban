namespace Featureban.Domain.Positions
{
    public abstract class Position
    {
        protected readonly int _totalSteps;

        protected Position(int totalSteps)
        {
            _totalSteps = totalSteps;
        }

        public abstract Position NextPosition();

        public override bool Equals(object obj)
        {
            var other = obj as Position;

            if ((object) other == null)
                return false;

            return GetType() == other.GetType();
        }

        public abstract override int GetHashCode();

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

            return p1.Equals(p2);
        }

        public static bool operator != (Position p1, Position p2)
        {
            return !(p1 == p2);
        }

        public static Position ToDo()
        {
            return new PositionToDo(0);
        }

        public static Position Done()
        {
            return new PositionDone();
        }
    }
}