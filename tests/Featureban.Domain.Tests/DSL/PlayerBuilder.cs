namespace Featureban.Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private Player _player;
        private StickersBoard _stickersBoard;

        public PlayerBuilder()
        {
            _stickersBoard = new StickersBoard(new Scale(2));
            _player = new Player(_stickersBoard);
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