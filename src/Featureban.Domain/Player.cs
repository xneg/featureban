﻿using Featureban.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    public class Player
    {
        private readonly List<Sticker> _stickers = new List<Sticker>();
        private readonly Queue<Token> _tokens = new Queue<Token>();
        private readonly StickersBoard _stickersBoard;
        private readonly ICoin _coin;

        public IEnumerable<Sticker> Stickers => _stickers;

        public IReadOnlyList<Token> Tokens => _tokens.ToList().AsReadOnly();

        public Player(StickersBoard stickersBoard, ICoin coin)
        {
            _stickersBoard = stickersBoard;
            _coin = coin;
        }

        public void TakeStickerToWork()
        {
            _stickersBoard.TakeStickerInWorkFor(this);
        }

        public void MakeToss()
        {
            var token = _coin.MakeToss();
            _tokens.Enqueue(token);
        }
        public void AddToken(Token token)
        {
            _tokens.Enqueue(token);
        }

        public void SpendToken()
        {
            if (_tokens.Count() == 0)
            {
                return;
            }

            var currentToken = _tokens.Dequeue();

            if (currentToken.IsEagle)
            {
                BlockStiker();

                TakeStickerToWork();
            }

            if (currentToken.IsTails)
            {
                if (IsAnyStickerNotBlocked())
                {
                    MoveSticker();
                }
                else if (IsAnyStickerBlocked())
                {
                    UnblockSticker();
                }
                else
                {
                    TakeStickerToWork();
                }
            }
        }

        private void BlockStiker()
        {
            var sticker = _stickersBoard.GetUnblockedStickerFor(this);
                

            sticker?.Block();
        }

        private void MoveSticker()
        {
            var unblockedSticker = _stickersBoard.GetUnblockedStickerFor(this);                

            if (unblockedSticker != null)
            {
                _stickersBoard.StepUp(unblockedSticker);
                return;
            }
        }

        private void UnblockSticker()
        {
            var blockedSticker = _stickersBoard.GetBlockedStickerFor(this);                    

            if (blockedSticker != null)
            {
                blockedSticker.Unblock();
                return;
            }
        }

        public void GiveTokenTo(Player player)
        {
            if (_tokens.Any(t => t.IsEagle))
                throw new InvalidOperationException();

            SpendToken();
            player.AddToken(new Token(TokenType.Tails));
        }

        private bool IsAnyStickerBlocked()
        {
            return _stickers.Any(s => s.Blocked);
        }

        private bool IsAnyStickerNotBlocked()
        {
            return _stickers.Any(s => !s.Blocked);
        }
    }
}