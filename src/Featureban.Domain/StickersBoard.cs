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

        public Sticker GetBlockedStickerFor(Player player)
        {
           return 
                _progressSteps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && s.Blocked);
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

        public Sticker CreateStickerInProgressin(ProgressPosition progressPosition, Sticker sticker)
        {
            if (CanMoveTo(progressPosition))
            {
                _progressSteps[progressPosition].Add(sticker);
            }

            return sticker;
        }

        public void CreateStickerInProgress(Sticker sticker)
        {     
            if (CanCreateStickerInProgress())
            {
                _progressSteps[ProgressPosition.First()].Add(sticker);
            }
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return
                _progressSteps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && !s.Blocked);
        }

        public void StepUp (Sticker sticker)
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

        public bool CanCreateStickerInProgress()
        {
            return CanMoveTo(ProgressPosition.First());
        }

        public Sticker GetMoveableStickerFor(Player player)
        {
            return _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => s.Owner == player && !s.Blocked &&
                    (!_scale.IsValid(s.ProgressPosition.Next()) || CanMoveTo(s.ProgressPosition.Next())));
        }

        public void Setup(IEnumerable<Player> players)
        {
            foreach(var player in players)
            {
                var sticker = new Sticker(player);
                _progressSteps[ProgressPosition.First()].Add(sticker);
            }
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

        public IEnumerable<Sticker> GetStickersIn(ProgressPosition progressPosition)
        {
            return _progressSteps[progressPosition].AsReadOnly();
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
        }

        public bool CanMoveTo(ProgressPosition position)
        {
            return _wip == null || _progressSteps[position].Count < _wip;
        }

        public bool CanMoveToNew(ProgressPosition position)
        {
            return !GetProgressCell(position).IsFull;
        }

        private ProgressCell GetProgressCell(ProgressPosition position)
        {
            return _progressCells.FirstOrDefault(p => p.Position == position);
        }
    }
}