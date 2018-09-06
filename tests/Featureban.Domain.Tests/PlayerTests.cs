using Featureban.Domain.Interfaces;
using Featureban.Domain.Tests.DSL;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerCanGetToken_WhenMakeToss()
        {
            var player = Create.Player().Please();

            player.MakeToss();

            Assert.Equal(1, player.Tokens.Count);
        }

        [Fact]
        public void PlayerGainsEagleToken_WhenCoinIsEagle()
        {
            var coinStub = new Mock<ICoin>();
            var player = Create.Player().WithCoin(coinStub.Object).Please();

            coinStub.Setup(c => c.MakeToss()).Returns(Token.CreateEagleToken());

            player.MakeToss();

            var token = player.Tokens.Single();
            Assert.True(token.IsEagle);
        }

        [Fact]
        public void PlayerWithTokenLoseToken_WhenSpendToken()
        {
            var player = Create.Player().WithTokens(1.Token()).Please();

            player.SpendToken();

            Assert.Equal(0, player.Tokens.Count);
        }

        [Fact]
        public void PlayerCanNotGiveToken_WhenHasEagleToken()
        {
            var player = Create.Player().WithEagleToken().Please();

            Assert.Throws<InvalidOperationException>(() => player.GiveTokenToPull());
        }

        [Fact]
        public void PlayerLoseToken_WhenGivesTailsToken()
        {
            var player = Create.Player().WithTailsToken().Please();

            player.GiveTokenToPull();

            Assert.Equal(0, player.Tokens.Count);
        }

        // todo: Этот тест нужен. Переписать с DSL.

        //[Fact]
        //public void PlayerGainsToken_WhenTakeItFromPull()
        //{
        //    var player = Create.Player().WithTailsToken().Please();
        //    var player2 = Create.Player().Please();

        //    player.TakeTokenFromPull();

        //    Assert.Equal(1, player2.Tokens.Count);
        //}

        [Fact]
        public void PlayerBlocksUnblockedSticker_WhenSpendingEagleToken()
        {
            var boardMock = new Mock<IStickersBoard>();
            var player = Create.Player().WithBoard(boardMock.Object).WithEagleToken().Please();
            var sticker = Create.Sticker().Please();
            boardMock.Setup(b => b.GetUnblockedStickerFor(player)).Returns(sticker);

            player.SpendToken();

            Assert.True(sticker.Blocked);
        }

        [Fact]
        public void PlayerTakesNewSticker_WhenSpendingEagleToken()
        {
            var boardMock = new Mock<IStickersBoard>();
            var player = Create.Player().WithBoard(boardMock.Object).WithEagleToken().Please();

            player.SpendToken();

            boardMock.Verify(b => b.TakeStickerInWorkFor(player), Times.Once);
        }

        [Fact]
        public void PlayerMovesUnblockedSticker_WhenSpendingTailsToken()
        {
            var boardMock = new Mock<IStickersBoard>();
            var player = Create.Player().WithBoard(boardMock.Object).WithTailsToken().Please();
            var sticker = Create.Sticker().Please();
            boardMock.Setup(b => b.GetUnblockedStickerFor(player)).Returns(sticker);

            player.SpendToken();

            boardMock.Verify(b => b.StepUp(sticker), Times.Once);
        }

        [Fact]
        public void PlayerUnblockBlockedSticker_WhenSpendingTailsToken()
        {
            var boardMock = new Mock<IStickersBoard>();
            var player = Create.Player().WithBoard(boardMock.Object).WithTailsToken().Please();
            var sticker = Create.Sticker().Blocked().Please();
            boardMock.Setup(b => b.GetUnblockedStickerFor(player)).Returns<Sticker>(null);
            boardMock.Setup(b => b.GetBlockedStickerFor(player)).Returns(sticker);

            player.SpendToken();

            Assert.False(sticker.Blocked);
        }

        [Fact]
        public void PlayerTakesNewSticker_WhenSpendingTailsToken()
        {
            var boardMock = new Mock<IStickersBoard>();
            var player = Create.Player().WithBoard(boardMock.Object).WithTailsToken().Please();
            boardMock.Setup(b => b.GetUnblockedStickerFor(player)).Returns<Sticker>(null);
            boardMock.Setup(b => b.GetBlockedStickerFor(player)).Returns<Sticker>(null);

            player.SpendToken();

            boardMock.Verify(b => b.TakeStickerInWorkFor(player), Times.Once);
        }
    }
}
