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

        public static GameBuilder Game()
        {
            return new GameBuilder();
        }

        public static StickersBoardBuilder StickersBoard()
        {
            return  new StickersBoardBuilder();
        }

        public static StickerBoardParser StickersBoard(string state)
        {
            return new StickerBoardParser(state);
        }

        public static TokensPullBuilder TokenPull()
        {
            return new TokensPullBuilder();
        }
    }
}
