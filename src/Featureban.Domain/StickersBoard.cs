using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class StickersBoard : IStickersBoard
    {
        protected readonly Scale _scale;

        protected readonly List<ProgressCell> _progressCells;

        public int DoneStickers { get; private set; }

        public StickersBoard(Scale scale, int? wip = null)
        {
            _scale = scale;
            _progressCells = new List<ProgressCell>();

            CreateCells(_scale, wip);
        }

        public void Setup(IEnumerable<Player> players)
        {
            var firstCell = GetProgressCell(ProgressPosition.First());
            foreach (var player in players)
            {
                var sticker = new Sticker(player);
                firstCell.Add(sticker);
            }
        }

        public Sticker CreateStickerInProgress(Player player)
        {
            var sticker = new Sticker(player);

            if (CanCreateStickerInProgress())
            {
                GetProgressCell(ProgressPosition.First()).Add(sticker);
            }

            return sticker;
        }

        public Sticker CreateStickerInPosition(ProgressPosition progressPosition, Sticker sticker)
        {
            if (CanMoveTo(progressPosition))
            {
                GetProgressCell(progressPosition).Add(sticker);
            }

            return sticker;
        }

        public Sticker GetBlockedStickerFor(Player player)
        {
            return ReversedProgressCells
                .Select(c => c.GetBlockedStickerFor(player))
                 .FirstOrDefault(s => s != null);
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return ReversedProgressCells
                .Select(c => c.GetUnblockedStickerFor(player))
                .FirstOrDefault(s => s != null);
        }
        
        public Player GetPlayerThatCanSpendToken()
        {
            var movableSticker =
                ReversedProgressCells
                .Select(c => c.GetUnblockedSticker())
                .FirstOrDefault(s => s != null && CanMove(s));

            if (movableSticker != null)
                return movableSticker.Owner;

            var blockedSticker = ReversedProgressCells
                .Select(c => c.GetBlockedSticker())
                .FirstOrDefault(s => s != null);

            if (blockedSticker != null)
                return blockedSticker.Owner;

            return null;
        }
          
        public Sticker GetMoveableStickerFor(Player player)
        {
            return ReversedProgressCells
                .Select(c => c.GetUnblockedStickerFor(player))
                .FirstOrDefault(s => s != null && CanMove(s));
        }

        public IEnumerable<Sticker> GetStickersIn(ProgressPosition progressPosition)
        {
            return GetProgressCell(progressPosition).Stickers;
        }

        public bool CanCreateStickerInProgress()
        {
            return CanMoveTo(ProgressPosition.First());
        }

        public bool CanMoveTo(ProgressPosition position)
        {
            return !GetProgressCell(position).IsFull;
        }

        public void StepUp(Sticker sticker)
        {
            if (sticker.Blocked)
            {
                return;
            }

            var oldPosition = sticker.ProgressPosition;
            var newPosition = oldPosition.Next();

            if (_scale.IsValid(newPosition))
            {
                if (CanMoveTo(newPosition))
                {
                    GetProgressCell(oldPosition).Remove(sticker);
                    GetProgressCell(newPosition).Add(sticker);
                }
            }
            else
            {
                GetProgressCell(oldPosition).Remove(sticker);
                DoneStickers++;
            }
        }

        private void CreateCells(Scale scale, int? wip)
        {
            var position = ProgressPosition.First();

            while (scale.IsValid(position))
            {
                _progressCells.Add(new ProgressCell(position, wip));
                position = position.Next();
            }
        }

        private ProgressCell GetProgressCell(ProgressPosition position)
        {
            return _progressCells.FirstOrDefault(p => p.Position == position);
        }

        private bool CanMove(Sticker s)
        {
            var newPosition = s.ProgressPosition.Next();
            return !_scale.IsValid(newPosition) || CanMoveTo(newPosition);
        }

        private IOrderedEnumerable<ProgressCell> ReversedProgressCells => 
            _progressCells.OrderByDescending(c => c.Position.Step);
    }
}