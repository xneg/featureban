using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class StickersBoard : IStickersBoard
    {
        protected readonly Scale _scale;
        protected readonly int? _wip;

        protected readonly List<ProgressCell> _progressCells;
        protected readonly Dictionary<ProgressPosition, List<Sticker>> _progressSteps;

        public int DoneStickers { get; private set; }

        public StickersBoard(Scale scale, int? wip = null)
        {
            _scale = scale;
            _progressSteps = new Dictionary<ProgressPosition, List<Sticker>>();
            _progressCells = new List<ProgressCell>();
            _wip = wip;

            CreateNewPartitions(_scale, _wip);
        }

        public void Setup(IEnumerable<Player> players)
        {
            var firstCell = GetProgressCell(ProgressPosition.First());
            foreach (var player in players)
            {
                var sticker = new Sticker(player);
                _progressSteps[ProgressPosition.First()].Add(sticker);
                firstCell.Add(sticker);
            }
        }

        public Sticker CreateStickerInProgress(Player player)
        {
            var sticker = new Sticker(player);

            if (CanCreateStickerInProgress())
            {
                _progressSteps[ProgressPosition.First()].Add(sticker);
            }

            return sticker;
        }

        public Sticker CreateStickerInProgressNew(Player player)
        {
            var sticker = new Sticker(player);

            if (CanCreateStickerInProgressNew())
            {
                GetProgressCell(ProgressPosition.First()).Add(sticker);
            }

            return sticker;
        }

        public Sticker CreateStickerInPosition(ProgressPosition progressPosition, Sticker sticker)
        {
            if (CanMoveTo(progressPosition))
            {
                _progressSteps[progressPosition].Add(sticker);
            }

            return sticker;
        }

        public Sticker CreateStickerInPositionNew(ProgressPosition progressPosition, Sticker sticker)
        {
            if (CanMoveToNew(progressPosition))
            {
                GetProgressCell(progressPosition).Add(sticker);
            }

            return sticker;
        }

        public Sticker GetBlockedStickerFor(Player player)
        {
            return
                 _progressSteps
                 .SelectMany(p => p.Value)
                 // todo: сделать сортировку по позиции
                 .FirstOrDefault(s => s.Owner == player && s.Blocked);
        }

        public Sticker GetBlockedStickerForNew(Player player)
        {
            return  _progressCells
                .Select(c => c.GetBlockedStickerFor(player))
                .FirstOrDefault();
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return
                _progressSteps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && !s.Blocked);
        }

        public Sticker GetUnblockedStickerForNew(Player player)
        {
            return _progressCells
                .Select(c => c.GetUnblockedStickerFor(player))
                .FirstOrDefault();
        }

        public Player GetPlayerThatCanSpendToken()
        {
            var movableSticker =
                _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => !s.Blocked &&
                    (!_scale.IsValid(s.ProgressPosition.Next()) || CanMoveTo(s.ProgressPosition.Next())));

            if (movableSticker != null)
                return movableSticker.Owner;

            var blockedSticker =
                _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => s.Blocked);

            if (blockedSticker != null)
                return blockedSticker.Owner;

            return null;
        }

        public Player GetPlayerThatCanSpendTokenNew()
        {
            var movableSticker =
                _progressCells
                .Select(c => c.GetUnblockedSticker())
                .FirstOrDefault(s => s != null && CanMove(s));

            if (movableSticker != null)
                return movableSticker.Owner;

            var blockedSticker = _progressCells
                .Select(c => c.GetBlockedSticker())
                .FirstOrDefault();

            if (blockedSticker != null)
                return blockedSticker.Owner;

            return null;
        }
          
        public Sticker GetMoveableStickerFor(Player player)
        {
            return _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => s.Owner == player && !s.Blocked &&
                    (!_scale.IsValid(s.ProgressPosition.Next()) || CanMoveTo(s.ProgressPosition.Next())));
        }

        public Sticker GetMoveableStickerForNew(Player player)
        {
            return _progressCells
                .Select(c => c.GetUnblockedStickerFor(player))
                .FirstOrDefault(s => s != null && CanMove(s));
        }

        public IEnumerable<Sticker> GetStickersIn(ProgressPosition progressPosition)
        {
            return _progressSteps[progressPosition].AsReadOnly();
        }

        public IEnumerable<Sticker> GetStickersInNew(ProgressPosition progressPosition)
        {
            return GetProgressCell(progressPosition).Stickers;
        }

        public bool CanCreateStickerInProgress()
        {
            return CanMoveTo(ProgressPosition.First());
        }

        public bool CanCreateStickerInProgressNew()
        {
            return CanMoveToNew(ProgressPosition.First());
        }

        public bool CanMoveTo(ProgressPosition position)
        {
            return _wip == null || _progressSteps[position].Count < _wip;
        }

        public bool CanMoveToNew(ProgressPosition position)
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
                    _progressSteps[oldPosition].Remove(sticker);
                    _progressSteps[newPosition].Add(sticker);

                    sticker.ChangePosition(newPosition);
                }
                if (CanMoveToNew(newPosition))
                {
                    GetProgressCell(oldPosition).Remove(sticker);
                    GetProgressCell(newPosition).Add(sticker);
                }
            }
            else
            {
                _progressSteps[oldPosition].Remove(sticker);
                GetProgressCell(oldPosition).Remove(sticker);
                DoneStickers++;
            }
        }

        private void CreateNewPartitions(Scale scale, int? wip)
        {
            var position = ProgressPosition.First();

            while (scale.IsValid(position))
            {
                _progressSteps[position] = new List<Sticker>();
                _progressCells.Add(new ProgressCell(position, wip));
                position = position.Next();
            }
            _progressCells.Reverse();
        }

        private ProgressCell GetProgressCell(ProgressPosition position)
        {
            return _progressCells.FirstOrDefault(p => p.Position == position);
        }

        private bool CanMove(Sticker s)
        {
            var newPosition = s.ProgressPosition.Next();
            return !_scale.IsValid(newPosition) || CanMoveToNew(newPosition);
        }
    }
}