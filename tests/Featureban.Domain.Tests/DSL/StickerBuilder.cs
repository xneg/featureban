using System;

namespace Featureban.Domain.Tests.DSL
{
    public class StickerBuilder
    {        
        private Scale _scale;
        private Player _player;
        private StickersBoard _stickersBoard;
        private TokensPull _tokensPull;

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