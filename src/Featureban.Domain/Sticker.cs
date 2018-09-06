namespace Featureban.Domain
{
    public class Sticker
    {
        public Player Owner { get; }

        public bool Blocked { get; private set; }

        public ProgressPosition ProgressPosition { get; private set; }

        public StickerStatus Status { get; private set; }

        public Sticker(Player player)
        {
            Owner = player;
            ProgressPosition = ProgressPosition.First();
            Status = StickerStatus.Todo;
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unblock()
        {
            Blocked = false;
        }

        public void ChangePositionNew(ProgressPosition progressPosition)
        {
            ProgressPosition = progressPosition;
        }

        public void ChangeStatus(StickerStatus status)
        {
            Status = status;
        }
    }
}