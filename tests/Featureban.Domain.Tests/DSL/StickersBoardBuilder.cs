using System;

namespace Featureban.Domain.Tests.DSL
{
    internal class StickersBoardBuilder
    {
        private Scale _scale;
        private int? _wip;
        private int _stickersInProgress;
        private Player _player;

        public StickersBoardBuilder()
        {
            _scale = new Scale(2);
            _player = Create.Player().Please();
        }

        public StickersBoardBuilder WithScale(int positionsInProgress)
        {
            _scale = new Scale(positionsInProgress);
            return this;
        }

        public StickersBoardBuilder WithWip(int wipCount)
        {
            _wip = wipCount;
            return this;
        }
        public StickersBoard Please()
        {
            var stickerBoard =  new StickersBoard(_scale, _wip);
            for (int i = 0; i < _stickersInProgress; i++)
            {
                stickerBoard.TakeStickerInWorkFor(_player);
            }

            return stickerBoard;
        }

        public  StickersBoardBuilder WithStickerInProgress()
        {
            _stickersInProgress++;
            return this;
        }

        public StickersBoardBuilder WithStickerInProgressFor(Player player)
        {
            _stickersInProgress++;
            _player = player;
            return this;
        }
    }
}