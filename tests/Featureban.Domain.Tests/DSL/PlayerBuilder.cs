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

        public PlayerBuilder WithToken(Token token)
        {           
            _player.AddToken(token);
            
            return this;
        }

        public Player Please()
        {            
            return _player;
        }
    }
}