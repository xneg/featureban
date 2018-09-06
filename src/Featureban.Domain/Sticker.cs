using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class Sticker
    {
        public Player Owner { get; }

        public bool Blocked { get; private set; }

        public Position Position { get; private set; }

        public PositionNew PositionNew { get; private set; }

        public Sticker(Player player, Position position) : this(player)
        {
            Owner = player;
            Position = position;
        }

        public Sticker(Player player)
        {
            Owner = player;
            PositionNew = PositionNew.First();
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unblock()
        {
            Blocked = false;
        }

        public void ChangePosition(Position position)
        {
            Position = position;
        }

        public void ChangePositionNew(PositionNew position)
        {
            PositionNew = position;
        }
    }
}