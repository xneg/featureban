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

        public IEnumerable<Sticker> Stickers => _stickers;

        public IReadOnlyList<Token> Tokens => _tokens.ToList().AsReadOnly();

        public Player(StickersBoard stickersBoard)
        {
            _stickersBoard = stickersBoard;
        }

        public void TakeStickerToWork(Sticker sticker)
        {
            sticker.StepUp();
            _stickers.Add(sticker);
        }

        public void MakeToss()
        {
            _tokens.Enqueue(new Token(TokenType.Tails));
        }
        public void AddToken(Token token)
        {
            _tokens.Enqueue(token);
        }

        public void SpendToken()
        {
            var tokenType = _tokens.Dequeue().TokenType;

            if (tokenType == TokenType.Eagle)
            {
                BlockStiker();

                TakeStickerToWork(_stickersBoard.CreateSticker());
            }

            if (tokenType == TokenType.Tails)
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
                    TakeStickerToWork(_stickersBoard.CreateSticker());
                }
            }
        }

        private void BlockStiker()
        {
            var sticker = _stickers
                .Where(s => s.Status == PositionStatus.InProgress &&
                       !s.Blocked)
                .OrderByDescending(s => s.StepInProgress)
                .FirstOrDefault();

            sticker?.Block();
        }

        private void MoveSticker()
        {
            var unblockedSticker = _stickers
                    .Where(s => s.Status == PositionStatus.InProgress && !s.Blocked)
                    .OrderByDescending(s => s.StepInProgress)
                    .FirstOrDefault();

            if (unblockedSticker != null)
            {
                unblockedSticker.StepUp();
                return;
            }
        }

        private void UnblockSticker()
        {
            var blockedSticker = _stickers
                    .Where(s => s.Status == PositionStatus.InProgress && s.Blocked)
                    .OrderByDescending(s => s.StepInProgress)
                    .FirstOrDefault();

            if (blockedSticker != null)
            {
                blockedSticker.Unblock();
                return;
            }
        }

        public void GiveTokenTo(Player player)
        {
            if (_tokens.Any(t => t.TokenType == TokenType.Eagle))
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