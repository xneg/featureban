﻿using Featureban.Domain.Interfaces;
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

            coinStub.Setup(c => c.MakeToss()).Returns(new Token(TokenType.Eagle));

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
            var player2 = Create.Player().Please();

            Assert.Throws<InvalidOperationException>(() => player.GiveTokenTo(player2));
        }

        [Fact]
        public void PlayerLoseToken_WhenGivesTailsToken()
        {
            var player = Create.Player().WithTailsToken().Please();
            var player2 = Create.Player().Please();

            player.GiveTokenTo(player2);

            Assert.Equal(0, player.Tokens.Count);
        }

        [Fact]
        public void PlayerGainsToken_WhenOtherPlayGivesHimTailsToken()
        {
            var player = Create.Player().WithTailsToken().Please();
            var player2 = Create.Player().Please();

            player.GiveTokenTo(player2);

            Assert.Equal(1, player2.Tokens.Count);
        }

        //[Fact]
        //public void PlayerBlockSticker_WhenSpendEagleToken()
        //{
        //    var sticker = Create.Sticker().Please();
        //    var player = Create.Player().WithEagleToken().Please();

        //    player.SpendToken();

        //    Assert.True(sticker.Blocked);
        //}

        //[Fact]
        //public void PlayerTakesNewSticker_WhenSpendEagleToken()
        //{
        //    var player = Create.Player().WithEagleToken().Please();

        //    player.SpendToken();

        //    Assert.Single(player.Stickers);
        //}

        //[Fact]
        //public void PlayerUnblockedSticker_WhenSpendTailsToken()
        //{
        //    var sticker = Create.Sticker().Please();
        //    var player = Create.Player().WithTailsToken().Please();
        //    sticker.Block();

        //    player.SpendToken();

        //    Assert.False(sticker.Blocked);
        //}

        //[Fact]
        //public void PlayerTakesNewSticker_WhenSpendTailsToken()
        //{
        //    var player = Create.Player().WithTailsToken().Please();

        //    player.SpendToken();

        //    Assert.Single(player.Stickers);
        //}

    }
}
