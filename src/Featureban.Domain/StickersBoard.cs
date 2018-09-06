using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    public class StickersBoard
    {
        private readonly Scale _scale;
        private readonly int? _wip;

        private readonly Dictionary<Position, List<Sticker>> _partitions;

        public StickersBoard(Scale scale, int? wip = null)
        {
            _scale = scale;
            _partitions = new Dictionary<Position, List<Sticker>>();
            _wip = wip;

            CreatePartitions(_scale);
        }

        public Sticker CreateStickerFor(Player player)
        {
            var sticker = new Sticker(player, _scale.CreatePositionToDo());

            _partitions[Position.ToDo()].Add(sticker);

            return sticker;
        }

        public IEnumerable<Sticker> GetStickersIn(Position position)
        {
            return _partitions[position].AsReadOnly();
        }

        public Sticker GetBlockedStickerFor(Player player)
        {
           return _partitions
                .Where(p => p.Key is PositionInProgress)
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && s.Blocked);
        }

        public void TakeStickerInWorkFor(Player player)
        {
            var sticker = CreateStickerFor(player);
            StepUp(sticker);
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return 
                _partitions
                .Where(p => p.Key is PositionInProgress)
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && !s.Blocked);
        }

        public void StepUp (Sticker sticker)
        {
            if(sticker.Blocked)
            {
                return;
            }

            var oldPosition = sticker.Position;
            var newPosition = oldPosition.NextPosition();

            if(_partitions[newPosition].Count ==  _wip)
            {
                return;
            }

            _partitions[oldPosition].Remove(sticker);
            _partitions[newPosition].Add(sticker);

            sticker.ChangePosition(newPosition);
        }

        private void CreatePartitions(Scale scale)
        {
            var position = scale.CreatePositionToDo();

            do
            {
                _partitions[position] = new List<Sticker>();
                position = position.NextPosition();

            } while (position != Position.Done());

            _partitions[position] = new List<Sticker>();
        }
    }
}