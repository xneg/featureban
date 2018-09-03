using System;

namespace Featureban.Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private Player _player;
        private StickersFactory _stickersFactory;

        public PlayerBuilder()
        {
            _stickersFactory = new StickersFactory(new Scale(2));
            _player = new Player(_stickersFactory);
        }
        
        public PlayerBuilder With(Sticker sticker)
        {
            _player.TakeStickerToWork(sticker);
            return this;
        }

        public PlayerBuilder WithTokens(int tokenCount)
        {
            for (var i = 0; i < tokenCount; i++)
            {
                _player.AddToken(new Token(TokenType.Eagle));
            }

            return this;
        }       

        public Player Please()
        {            
            return _player;
        }

        public PlayerBuilder WithTailsToken()
        {
            _player.AddToken(new Token(TokenType.Tails));
            return this;
        }

        public PlayerBuilder WithEagleToken()
        {
            _player.AddToken(new Token(TokenType.Eagle));
            return this;
        }
    }
}