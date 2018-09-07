using System;

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

        public static StickersBoardBuilder StickersBoard(string state)
        {
            var stickerBoardBuilderParser = new StickerBoardParser();
            return stickerBoardBuilderParser.Parse(state);
        }

        public static TokensPullBuilder TokenPull()
        {
            return new TokensPullBuilder();
        }
    }
}
