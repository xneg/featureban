﻿using Featureban.Domain.Tests.DSL;
using System;
using System.Linq;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerHasSticker_WhenAddSticker()
        {
            var sticker = Create.Sticker().Please();
            var player = Create.Player().Please();

            player.TakeStickerToWork(sticker);

            Assert.Single(player.Stickers, sticker);
        }

        [Fact]
        public void PlayerCanGetToken_WhenMakeToss()
        {
            var player = Create.Player().Please();

            player.MakeToss();

            Assert.Equal(1, player.Tokens.Count);
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
            var eagleToken = Create.Token().Eagle().Please();
            var player = Create.Player().WithToken(eagleToken).Please();
            var player2 = Create.Player().Please();

            Assert.Throws<InvalidOperationException>(() => player.GiveTokenTo(player2));
        }

        [Fact]
        public void PlayerLoseToken_WhenGivesTailsToken()
        {
            var tailsToken = Create.Token().Tails().Please();
            var player = Create.Player().WithToken(tailsToken).Please();
            var player2 = Create.Player().Please();

            player.GiveTokenTo(player2);

            Assert.Equal(0, player.Tokens.Count);
        }

        [Fact]
        public void PlayerGainsToken_WhenOtherPlayGivesHimTailsToken()
        {
            var tailsToken = Create.Token().Tails().Please();
            var player = Create.Player().WithToken(tailsToken).Please();
            var player2 = Create.Player().Please();

            player.GiveTokenTo(player2);

            Assert.Equal(1, player2.Tokens.Count);
        }

        [Fact]
        public void PlayerBlockSticker_WhenSpendEagleToken()
        {
            var sticker = Create.Sticker().Please();
            var eagleToken = Create.Token().Eagle().Please();
            var player = Create.Player().WithToken(eagleToken).With(sticker).Please();

            player.SpendToken();

            Assert.True(sticker.Blocked);
        }

        [Fact]
        public void PlayerTakesNewSticker_WhenSpendEagleToken()
        {
            var sticker = Create.Sticker().Please();
            var eagleToken = Create.Token().Eagle().Please();
            var player = Create.Player().WithToken(eagleToken).With(sticker).Please();

            player.SpendToken();

            Assert.Equal(2, player.Stickers.Count());
        }

        [Fact]
        public void PlayerStepUpUnblockedSticker_WhenSpendTailsToken()
        {
            var sticker = Create.Sticker().Please();
            var tailsToken = Create.Token().Tails().Please();
            var player = Create.Player().WithToken(tailsToken).With(sticker).Please();

            player.SpendToken();

            Assert.Equal(2, sticker.StepInProgress);
        }
    }
}
