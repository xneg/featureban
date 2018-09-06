namespace Featureban.Domain.Tests.DSL
{
    internal static class Create
    {
        public static StickerBuilder Sticker()
        {
            return new StickerBuilder();
        }

        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }

        public static StickersBoardBuilder StickersBoard()
        {
            return  new StickersBoardBuilder();
        }
    }
}
