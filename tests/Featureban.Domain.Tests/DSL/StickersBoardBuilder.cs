using Featureban.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain.Tests.DSL
{
    internal class StickersBoardBuilder
    {
        private Scale _scale;
        private int? _wip;
        private Dictionary<int, List<Sticker>> _stickersInProgress;
        private Player _player;
        private readonly Mock<IStickersBoard> _stickersBoardMock;

        public StickersBoardBuilder()
        {
            int positionsInProgress = 2;
            _scale = new Scale(positionsInProgress);
            _stickersInProgress = new Dictionary<int, List<Sticker>>();
            _player = Create.Player().Please();
            _stickersBoardMock =  new Mock<IStickersBoard>();

            for(var d=0; d<positionsInProgress; d++)
            {
                _stickersInProgress.Add(d, new List<Sticker>());
            }
        }

        public StickersBoardBuilder WithScale(int positionsInProgress)
        {
            _scale = new Scale(positionsInProgress);

            _stickersInProgress = new Dictionary<int, List<Sticker>>();
            for (var d = 0; d < positionsInProgress; d++)
            {
                _stickersInProgress.Add(d, new List<Sticker>());
            }

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
            for (var p = 0; p < _stickersInProgress.Keys.Count; p++)
            {
                for (int s = 0; s < _stickersInProgress[p].Count; s++)
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
            _stickersInProgress[0].Add(Create.Sticker().Please());
            return this;
        }

        public StickersBoardBuilder WithStickerInProgressFor(Player player)
        {
            _stickersInProgress[0].Add(Create.Sticker().For(player).Please());
            _player = player;
            return this;
        }

        public StickersBoardBuilder ThatAlwaysReturnUnblocked(Sticker sticker)
        {
            _stickersBoardMock.Setup(b => b.GetUnblockedStickerFor(It.IsAny<Player>())).Returns(sticker);
            return this;
        }

        public StickersBoardBuilder ThatAlwaysReturnMoveable(Sticker sticker)
        {
            _stickersBoardMock.Setup(b => b.GetMoveableStickerFor(It.IsAny<Player>())).Returns(sticker);
            return this;
        }

        public StickersBoardBuilder ThatAlwaysReturnBlocked(Sticker sticker)
        {
            _stickersBoardMock.Setup(b => b.GetBlockedStickerFor(It.IsAny<Player>())).Returns(sticker);
            return this;
        }

        public StickersBoardBuilder ThatNotReturnUnblocked()
        {
            _stickersBoardMock.Setup(b => b.GetUnblockedStickerFor(It.IsAny<Player>())).Returns((Sticker)null);
            return this;
        }

        public StickersBoardBuilder ThatNotReturnBlocked()
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

        public StickersBoardBuilder WithStickerInProgressForPosition(int position)
        {
            position--;
            _stickersInProgress[position].Add(Create.Sticker().Please());
            return this;
        }

        public StickersBoardBuilder WithBlockedStickerInProgressFor(Player player)
        {
            _stickersInProgress[0].Add(Create.Sticker().For(player).Please());
            _player = player;
            return this;
        }
    }
}