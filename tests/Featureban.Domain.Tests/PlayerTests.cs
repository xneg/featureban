﻿using Featureban.Domain.Interfaces;
using Featureban.Domain.Tests.DSL;
using Moq;
using System;
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

            Assert.NotNull(player.Token);
        }

        [Fact]
        public void PlayerGainsEagleToken_WhenCoinIsEagle()
        {            
            var player = Create.Player().WithAlwaysEagleCoin().Please();

            player.MakeToss();

            Assert.True(player.Token.IsEagle);
        }

        [Fact]
        public void PlayerWithTokenLoseToken_WhenSpendToken()
        {
            var player = Create.Player().WithTokens(1.Token()).Please();

            player.SpendToken();

            Assert.Null(player.Token);
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

            Assert.Null(player.Token);
        }
        
        [Fact]
        public void PlayerGainsToken_WhenTakeItFromPull()
        {
            var tokenPull = Create.TokenPull().With(1.Token()).Please();
            var player = Create.Player().With(tokenPull).Please();

            player.TakeTokenFromPull();

            Assert.NotNull(player.Token);
        }

        [Fact]
        public void PlayerBlocksUnblockedSticker_WhenSpendingEagleToken()
        {
            var sticker = Create.Sticker().Please();
            var stickersBoard = Create.StickersBoard().ThatAlwaysReturnUnblocked(sticker).Fast();
            var player = Create.Player().WithBoard(stickersBoard.Object).WithEagleToken().Please();

            player.SpendToken();

            Assert.True(sticker.Blocked);
        }

        [Fact]
        public void PlayerTakesNewSticker_WhenSpendingEagleToken()
        {
            var boardMock = new Mock<IStickersBoard>();
            var player = Create.Player().WithBoard(boardMock.Object).WithEagleToken().Please();

            player.SpendToken();

            boardMock.Verify(b => b.CreateStickerInProgress(player), Times.Once);
        }

        [Fact]
        public void PlayerMovesMoveableSticker_WhenSpendingTailsToken()
        {            
            var sticker = Create.Sticker().Please();
            var stickersBoard = Create.StickersBoard().ThatAlwaysReturnMoveable(sticker).Fast();
            var player = Create.Player().WithBoard(stickersBoard.Object).WithTailsToken().Please();

            player.SpendToken();

            stickersBoard.Verify(b => b.StepUp(sticker), Times.Once);
        }

        [Fact]
        public void PlayerUnblockBlockedSticker_WhenSpendingTailsToken()
        {
            var sticker = Create.Sticker().Blocked().Please();
            var stickersBoard = Create.StickersBoard()
                .ThatNotReturnUnblocked().And()
                .ThatAlwaysReturnBlocked(sticker).Fast();
            var player = Create.Player().WithBoard(stickersBoard.Object).WithTailsToken().Please();            

            player.SpendToken();

            Assert.False(sticker.Blocked);
        }

        [Fact]
        public void PlayerTakesNewSticker_WhenSpendingTailsToken()
        {
            var stickersBoard = Create.StickersBoard()
                .WithUnlimitedWip()
                .ThatNotReturnUnblocked().And()
                .ThatNotReturnBlocked().Fast();
            var player = Create.Player().WithBoard(stickersBoard.Object).WithTailsToken().Please();

            player.SpendToken();

            stickersBoard.Verify(b => b.CreateStickerInProgress(player), Times.Once);
        }
    }
}
