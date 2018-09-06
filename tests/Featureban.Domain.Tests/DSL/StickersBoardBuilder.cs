using Featureban.Domain.Interfaces;
using Moq;
using System;

namespace Featureban.Domain.Tests.DSL
{
    internal class StickersBoardBuilder
    {
        private Scale _scale;
        private int? _wip;
        private int _stickersInProgress;
        private Player _player;
        private Mock<IStickersBoard> _stickersBoardMock;

        public StickersBoardBuilder()
        {
            _scale = new Scale(2);
            _player = Create.Player().Please();
            _stickersBoardMock =  new Mock<IStickersBoard>();
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
                stickerBoard.CreateStickerInProgress(_player);
            }

            return stickerBoard;
        }

        public Mock<IStickersBoard> Fast()
        { 

            return _stickersBoardMock;
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

        public StickersBoardBuilder WhichAlwaysReturnUnblocked(Sticker sticker)
        {
            _stickersBoardMock.Setup(b => b.GetUnblockedStickerFor(It.IsAny<Player>())).Returns(sticker);
            return this;
        }

        public StickersBoardBuilder WhichAlwaysReturnMoveable(Sticker sticker)
        {
            _stickersBoardMock.Setup(b => b.GetMoveableStickerFor(It.IsAny<Player>())).Returns(sticker);
            return this;
        }

        public StickersBoardBuilder WhichAlwaysReturnBlocked(Sticker sticker)
        {
            _stickersBoardMock.Setup(b => b.GetBlockedStickerFor(It.IsAny<Player>())).Returns(sticker);
            return this;
        }

        public StickersBoardBuilder WhichNotReturnUnblocked()
        {
            _stickersBoardMock.Setup(b => b.GetUnblockedStickerFor(It.IsAny<Player>())).Returns((Sticker)null);
            return this;
        }

        public StickersBoardBuilder WhichNotReturnBlocked()
        {
            _stickersBoardMock.Setup(b => b.GetBlockedStickerFor(It.IsAny<Player>())).Returns((Sticker)null);
            return this;
        } 
        public StickersBoardBuilder WithUnlimitedWip()
        {
            _stickersBoardMock.Setup(b => b.CanCreateStickerInProgress()).Returns(true);
            return this;
        }

        public StickersBoardBuilder And()
        {
            return this;
        }
    }
}