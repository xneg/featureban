namespace Featureban.Domain.Tests.DSL
{
    internal class StickerBuilder
    {        
        private readonly Scale _scale;
        private readonly Player _player;
        private readonly StickersBoard _stickersBoard;
        private readonly TokensPull _tokensPull;

        private bool _blocked;

        public StickerBuilder()
        {
            _scale = new Scale(2);
            _stickersBoard = new StickersBoard(_scale);
            _tokensPull = new TokensPull();
            _player = new Player(_stickersBoard, new StubCoin(TokenType.Tails), _tokensPull);
        }

        public Sticker Please()
        {
            var sticker = _stickersBoard.CreateStickerFor(_player);

            if(_blocked)
            {
                sticker.Block();
            }
            return sticker;
        }

        public StickerBuilder Blocked()
        {
            _blocked = true;
            return this;
        }
    }
}