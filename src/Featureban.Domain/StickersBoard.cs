using System;
using System.Collections.Generic;
using Featureban.Domain.Positions;

namespace Featureban.Domain
{

    //Создать стикер
    //переместить стикер
    // вернуть стикеры игрока
    // ограничивать количесво стикеров в колонке 

    public class StickersBoard
    {
        private Scale _scale;
        private List<Sticker> _toDoList;

        public StickersBoard(Scale scale)
        {
            _scale = scale;
            _toDoList = new List<Sticker>();
        }

        public Sticker CreateStickerFor(Player player)
        {
            var sticker = new Sticker(player);
            _toDoList.Add(sticker);
            return sticker;
        }

        public IEnumerable<Sticker> GetStickersIn(Position position)
        {
            switch (position)
            {
                case PositionToDo _:
                    return _toDoList.AsReadOnly();
                default:
                    return null;

            }
        }

        public Sticker GetBlockedStickerFor(Player player)
        {
            throw new NotImplementedException();
        }

        public void TakeStickerInWorkFor(Player player)
        {
            throw new NotImplementedException();
        }

        internal Sticker GetUnblockedStickerFor(Player player)
        {
            throw new NotImplementedException();
        }

        internal void StepUp (Sticker unblockedSticker)
        {
            throw new NotImplementedException();
        }
    }
}