using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class Sticker
    {
        public bool Blocked { get; private set; }
        public Position Position { get; private set; }

        private Player _owner;

        public Player Owner => _owner;

        public Sticker(Player player, Position position)
        {
            _owner = player;
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