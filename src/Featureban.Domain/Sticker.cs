namespace Featureban.Domain
{
    public class Sticker
    {
        public bool Blocked { get; private set; }

        private Player _owner;

        public Player Owner => _owner;

        public Sticker()
        {

        }

        public Sticker(Player player)
        {
            _owner = player;
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unblock()
        {
            Blocked = false;
        }
    }
}