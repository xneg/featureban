using System;

namespace Featureban.Domain
{

    //Создать стикер
    //переместить стикер
    // вернуть стикеры игрока
    // ограничивать количесво стикеров в колонке 

    public class StickersBoard
    {
        private Scale _scale;

        public StickersBoard(Scale scale)
        {
            _scale = scale;
        }

        public Sticker CreateSticker()
        {
            var sticker = new Sticker();
            return sticker;
        }

        public Sticker CreateStickerFor(Player player)
        {
            var sticker = new Sticker(player);
            return sticker;
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