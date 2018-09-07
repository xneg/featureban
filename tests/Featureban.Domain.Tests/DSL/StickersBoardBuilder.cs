using Featureban.Domain.Interfaces;
using Moq;
using System.Collections.Generic;

namespace Featureban.Domain.Tests.DSL
{
    internal class StickersBoardBuilder
    {
        private Scale _scale;
        private int? _wip;
        private Dictionary<ProgressPosition, List<Sticker>> _stickersInProgress;
        private readonly Mock<IStickersBoard> _stickersBoardMock;

        public StickersBoardBuilder()
        {
            var positionsInProgress = 2;
            _scale = new Scale(positionsInProgress);
            _stickersInProgress = new Dictionary<ProgressPosition, List<Sticker>>();
            _stickersBoardMock = new Mock<IStickersBoard>();

            InitializeStickersInProgress(positionsInProgress);
        }

        public StickersBoardBuilder WithScale(int positionsInProgress)
        {
            _scale = new Scale(positionsInProgress);
            InitializeStickersInProgress(positionsInProgress);

            return this;
        }

        private void InitializeStickersInProgress(int positionsInProgress)
        {
            _stickersInProgress = new Dictionary<ProgressPosition, List<Sticker>>();
            var position = ProgressPosition.First();
            for (var d = 0; d < positionsInProgress; d++)
            {
                _stickersInProgress.Add(position, new List<Sticker>());
                position = position.Next();
            }
        }

        public StickersBoardBuilder WithWip(int wipCount)
        {
            _wip = wipCount;
            return this;
        }

        public StickersBoard Please()
        {
            var stickerBoard = new TestableStickersBoard(_scale, _wip);
            foreach(var position in _stickersInProgress.Keys)
            { 
                foreach (var sticker in _stickersInProgress[position])
                {
                    stickerBoard.CreateStickerInProgressin(position, sticker);
                }
            }

            return stickerBoard;
        }

        public Mock<IStickersBoard> Fast()
        {
            return _stickersBoardMock;
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
        
        public StickersBoardBuilder WithStickerInProgressFor(Player player, ProgressPosition position)
        {
            _stickersInProgress[position].Add(Create.Sticker().For(player).Please());
            return this;
        }        

        public StickersBoardBuilder WithBlockedStickerInProgressFor(Player player, ProgressPosition position)
        {
            var sticker = Create.Sticker().Blocked().For(player).Please();
            _stickersInProgress[position].Add(sticker);
            return this;
        }
    }
}