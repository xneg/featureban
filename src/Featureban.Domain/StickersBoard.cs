using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class StickersBoard : IStickersBoard
    {
        private readonly Scale _scale;
        private readonly int? _wip;

        private readonly Dictionary<PositionNew, List<Sticker>> _steps;

        public StickersBoard(Scale scale, int? wip = null)
        {
            _scale = scale;
            _steps = new Dictionary<PositionNew, List<Sticker>>();
            _wip = wip;

            CreateNewPartitions(_scale);
        }

        public Sticker CreateStickerFor(Player player)
        {
            var sticker = new Sticker(player);

            return sticker;
        }

        public IEnumerable<Sticker> GetStickersIn(PositionNew position)
        {
            return _steps[position].AsReadOnly();
        }

        public Sticker GetBlockedStickerFor(Player player)
        {
           return 
                _steps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && s.Blocked);
        }

        public void TakeStickerInWorkFor(Player player)
        {
            var sticker = CreateStickerFor(player);

            if (_steps[PositionNew.First()].Count != _wip)
            {
                _steps[PositionNew.First()].Add(sticker);
                sticker.ChangeStatus(StickerStatus.InProgress);
            }
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return
                _steps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && !s.Blocked);
        }

        public void StepUp (Sticker sticker)
        {
            StepUpNew(sticker);
        }

        private void CreateNewPartitions(Scale scale)
        {
            var position = PositionNew.First();

            while (scale.IsValid(position))
            {
                _steps[position] = new List<Sticker>();
                position = position.Next();
            }
        }

        private void StepUpNew(Sticker sticker)
        {
            if (sticker.Blocked)
            {
                return;
            }

            var oldPosition = sticker.PositionNew;
            var newPosition = oldPosition.Next();

            if (_scale.IsValid(newPosition))
            {
                if (_steps[newPosition].Count == _wip)
                {
                    return;
                }

                _steps[oldPosition].Remove(sticker);
                _steps[newPosition].Add(sticker);

                sticker.ChangePositionNew(newPosition);
            }
            else
            {
                _steps[oldPosition].Remove(sticker);
                sticker.ChangeStatus(StickerStatus.Done);
                // todo: sticker переходит в разряд завершенных
            }
        }
    }
}