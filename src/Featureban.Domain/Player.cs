using Featureban.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    public class Player
    {
        private readonly Queue<Token> _tokens = new Queue<Token>();
        private readonly IStickersBoard _stickersBoard;
        private readonly ICoin _coin;
        private readonly TokensPull _tokensPull;

        public IReadOnlyList<Token> Tokens => _tokens.ToList().AsReadOnly();

        public Player(IStickersBoard stickersBoard, ICoin coin, TokensPull tokensPull)
        {
            _stickersBoard = stickersBoard;
            _coin = coin;
            _tokensPull = tokensPull;
        }

        public void TakeStickerToWork()
        {
            _stickersBoard.TakeStickerInWorkFor(this);
        }

        public void MakeToss()
        {
            var token = _coin.MakeToss();
            AddToken(token);
        }

        public void AddToken(Token token)
        {
            _tokens.Enqueue(token);
        }

        public void SpendToken()
        {
            if (!_tokens.Any())
            {
                return;
            }

            var currentToken = _tokens.Dequeue();

            if (currentToken.IsEagle)
            {
                BlockSticker();

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

        public void TakeTokenFromPull()
        {
            if (_tokensPull.ContainsTokens)
            {
                _tokensPull.DecrementToken();
                AddToken(new Token(TokenType.Tails));            }
        }

        public void GiveTokenToPull()
        {
            if (_tokens.Any(t => t.IsEagle))
                throw new InvalidOperationException();

            SpendToken();
            _tokensPull.IncrementToken();
        }

        private void BlockSticker()
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
            }
        }

        private void UnblockSticker()
        {
            var blockedSticker = _stickersBoard.GetBlockedStickerFor(this);

            blockedSticker?.Unblock();
        }

        private bool IsAnyStickerBlocked()
        {
            return _stickersBoard.GetBlockedStickerFor(this) != null;
        }

        private bool IsAnyStickerNotBlocked()
        {
            return _stickersBoard.GetUnblockedStickerFor(this) != null;
        }
    }
}