using System.Collections.Generic;

namespace Featureban.Domain
{
    public class ProgressCell
    {
        private readonly List<Sticker> _stickers;

        private readonly int? _wip;

        public ProgressPosition Position { get; }

        public bool IsFull => _stickers.Count >= _wip;

        public ProgressCell(ProgressPosition progressPosition, int? wip)
        {
            _wip = wip;
            _stickers = new List<Sticker>();
            Position = progressPosition;
        }

        public void Add(Sticker sticker)
        {
            if (_wip == null || _stickers.Count < _wip)
                _stickers.Add(sticker);
        }

        public void Remove(Sticker sticker)
        {
            _stickers.Remove(sticker);
        }
    }
}
