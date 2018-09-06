namespace Featureban.Domain
{
    public sealed class PositionNew
    {
        public int Step { get; }

        public PositionNew(int step)
        {
            Step = step;
        }

        public PositionNew Next()
        {
            return new PositionNew(Step + 1);
        }

        public override bool Equals(object obj)
        {
            var p = obj as PositionNew;

            if ((object) p == null)
                return false;

            return p.Step == Step;
        }

        public bool Equals(PositionNew p)
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

        public static bool operator ==(PositionNew p1, PositionNew p2)
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

        public static bool operator !=(PositionNew p1, PositionNew p2)
        {
            return !(p1 == p2);
        }

        public static PositionNew First()
        {
            return new PositionNew(1);
        }
    }
}
