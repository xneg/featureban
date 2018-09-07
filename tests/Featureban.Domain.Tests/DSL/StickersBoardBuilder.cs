using Featureban.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;

namespace Featureban.Domain.Tests.DSL
{
    internal class StickersBoardBuilder
    {
        private Scale _scale;
        private int? _wip;
        private int[]  _stickersInProgress;
        private Player _player;
        private Mock<IStickersBoard> _stickersBoardMock;

        public StickersBoardBuilder()
        {
            int positionsInProgress = 2;
            _scale = new Scale(positionsInProgress);
            _stickersInProgress = new int[positionsInProgress];
            _player = Create.Player().Please();
            _stickersBoardMock =  new Mock<IStickersBoard>();
        }

        public StickersBoardBuilder WithScale(int positionsInProgress)
        {
            _scale = new Scale(positionsInProgress);
            _stickersInProgress = new int[positionsInProgress];
            return this;
        }

        public StickersBoardBuilder WithWip(int wipCount)
        {
            _wip = wipCount;
            return this;
        }

        public StickersBoard Please()
        {
            var stickerBoard = new StickersBoard(_scale, _wip);
            for (var p = 0; p < _stickersInProgress.Length; p++)
            {
                for (int s = 0; s < _stickersInProgress[p]; s++)
                {
                    var sticker = stickerBoard.CreateStickerInProgress(_player);
                    for (var m = 0; m < p; m++)
                    {                       
                        stickerBoard.StepUp(sticker);
                    }
                }
            }

            return stickerBoard;
        }

        public Mock<IStickersBoard> Fast()
        { 
            return _stickersBoardMock;
        }

        public  StickersBoardBuilder WithStickerInProgress()
        {
            _stickersInProgress[0]++;
            return this;
        }

        public StickersBoardBuilder WithStickerInProgressFor(Player player)
        {
            _stickersInProgress[0]++;
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

        public StickersBoardBuilder WithStickerInProgressPosition(int position)
        {
            position--;
            _stickersInProgress[position]++;
            return this;
        }
    }
}