using System;
using System.Collections.Generic;
using Featureban.Domain.Positions;

namespace Featureban.Domain
{
    // вернуть стикеры игрока
    // ограничивать количесво стикеров в колонке
    // не давать перемещать, если стикер заблокирован
    // не давать перемещать, если лимит

    public class StickersBoard
    {
        private Scale _scale;

        private Dictionary<Position, List<Sticker>> _partitions;

        public StickersBoard(Scale scale)
        {
            _scale = scale;
            _partitions = new Dictionary<Position, List<Sticker>>();

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
            throw new NotImplementedException();
        }

        public void TakeStickerInWorkFor(Player player)
        {
            throw new NotImplementedException();
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            throw new NotImplementedException();
        }

        public void StepUp (Sticker sticker)
        {
            var oldPosition = sticker.Position;
            var newPosition = oldPosition.NextPosition();

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