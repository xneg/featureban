using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class Sticker
    {
        public Player Owner { get; }

        public bool Blocked { get; private set; }

        public PositionNew PositionNew { get; private set; }

        public StickerStatus Status { get; private set; }

        public Sticker(Player player)
        {
            Owner = player;
            PositionNew = PositionNew.First();
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

        public void ChangePositionNew(PositionNew position)
        {
            PositionNew = position;
        }

        public void ChangeStatus(StickerStatus status)
        {
            Status = status;
        }
    }
}