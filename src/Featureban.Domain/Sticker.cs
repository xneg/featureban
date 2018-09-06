using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class Sticker
    {
        public bool Blocked { get; private set; }

        public Position Position { get; private set; }

        public Player Owner { get; }

        public Sticker(Player player, Position position)
        {
            Owner = player;
            Position = position;
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
    }
}