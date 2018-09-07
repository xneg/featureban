namespace Featureban.Domain.Tests.DSL
{
    internal class StickerBuilder
    {   
        private Player _player;
        private readonly StickersBoard _stickersBoard;
        private bool _blocked;

        public StickerBuilder()
        {
            _stickersBoard = Create.StickersBoard().Please();
            _player = Create.Player().Please();
        }

        public Sticker Please()
        {
            var sticker = _stickersBoard.CreateStickerInProgress(_player);

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

        public StickerBuilder For(Player player)
        {
            _player = player;
            return this;
        }
    }
}