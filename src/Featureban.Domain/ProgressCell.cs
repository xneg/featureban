using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    public class ProgressCell
    {
        private readonly List<Sticker> _stickers;

        public ProgressPosition Position { get; }

        public bool IsFull => _stickers.Count >= Wip;

        public int? Wip { get; }

        public IReadOnlyList<Sticker> Stickers => _stickers.AsReadOnly();

        public ProgressCell(ProgressPosition progressPosition, int? wip)
        {
            Wip = wip;
            _stickers = new List<Sticker>();
            Position = progressPosition;
        }

        public void Add(Sticker sticker)
        {
                _stickers.Add(sticker);
        }

        public void Remove(Sticker sticker)
        {
            _stickers.Remove(sticker);
        }

        public Sticker GetBlockedStickerFor(Player player)
        {
            return
                 _stickers.FirstOrDefault(s => s.Owner == player && s.Blocked);
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return
                _stickers.FirstOrDefault(s => s.Owner == player && !s.Blocked);
        }

        public Sticker GetBlockedSticker()
        {
            return
                 _stickers.FirstOrDefault(s => s.Blocked);
        }
        public Sticker GetUnblockedSticker()
        {
            return
                 _stickers.FirstOrDefault(s => !s.Blocked);
        }
    }
}
