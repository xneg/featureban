namespace Featureban.Domain
{
    public sealed class ProgressPosition
    {
        public int Step { get; }

        public ProgressPosition(int step)
        {
            Step = step;
        }

        public ProgressPosition Next()
        {
            return new ProgressPosition(Step + 1);
        }

        public override bool Equals(object obj)
        {
            var p = obj as ProgressPosition;

            if ((object) p == null)
                return false;

            return p.Step == Step;
        }

        public bool Equals(ProgressPosition p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return p.Step == Step;
        }

        public override int GetHashCode()
        {
            return Step;
        }

        public static bool operator ==(ProgressPosition p1, ProgressPosition p2)
        {
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            if (((object)p1 == null) || ((object)p2 == null))
            {
                return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(ProgressPosition p1, ProgressPosition p2)
        {
            return !(p1 == p2);
        }

        public static ProgressPosition First()
        {
            return new ProgressPosition(1);
        }
    }
}
