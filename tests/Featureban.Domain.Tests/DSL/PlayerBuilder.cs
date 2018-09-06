using Featureban.Domain.Interfaces;

namespace Featureban.Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private Player _player;
        private IStickersBoard _stickersBoard;
        private ICoin _coin;
        private readonly TokensPull _tokensPull;

        public PlayerBuilder()
        {
            _stickersBoard = new StickersBoard(new Scale(2));
            _coin = new StubCoin(Token.CreateTailsToken());
            _tokensPull = new TokensPull();

            _player = new Player(_stickersBoard, _coin, _tokensPull);
        }
        
        public PlayerBuilder WithTokens(int tokenCount)
        {
            for (var i = 0; i < tokenCount; i++)
            {
                _player.AddToken(Token.CreateEagleToken());
            }

            return this;
        }

        public PlayerBuilder WithBoard(IStickersBoard stickersBoard)
        {
            _stickersBoard = stickersBoard;

            _player = new Player(_stickersBoard, _coin, _tokensPull);

            return this;
        }

        public PlayerBuilder WithCoin(ICoin coin)
        {
            _coin = coin;
            _player = new Player(_stickersBoard, _coin, _tokensPull);

            return this;
        }

        public Player Please()
        {            
            return _player;
        }

        public PlayerBuilder WithTailsToken()
        {
            _player.AddToken(Token.CreateTailsToken());
            return this;
        }

        public PlayerBuilder WithEagleToken()
        {
            _player.AddToken(Token.CreateEagleToken());
            return this;
        }
    }
}