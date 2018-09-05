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
            var sticker = new Sticker(_scale);
            return sticker;
        }

        public Sticker CreateStickerFor(Player player)
        {
            var sticker = new Sticker(_scale, player);
            return sticker;
        }
    }
}