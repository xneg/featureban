using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    public class Player
    {
        private readonly List<Sticker> _stickers = new List<Sticker>();
        private readonly List<Token> _tokens = new List<Token>();
        private readonly StickersFactory _stickersFactory;

        public IEnumerable<Sticker> Stickers => _stickers;

        public IReadOnlyList<Token> Tokens => _tokens.AsReadOnly();

        public Player(StickersFactory stickersFactory)
        {
            _stickersFactory = stickersFactory;
        }

        public void TakeStickerToWork(Sticker sticker)
        {
            sticker.StepUp();
            _stickers.Add(sticker);
        }

        public void MakeToss()
        {
            _tokens.Add(new Token(TokenType.Tails));
        }        
        public void AddToken(Token token)
        {
            _tokens.Add(token);
        }

        public void SpendToken()
        {
            if(_tokens.FirstOrDefault()?.TokenType == TokenType.Eagle)
            {
                var sticker = _stickers.FirstOrDefault(s => 
                    s.Status == PositionStatus.InProgress && 
                    !s.Blocked);

                sticker?.Block();

                TakeStickerToWork(_stickersFactory.CreateSticker());
            }

            if (_tokens.FirstOrDefault()?.TokenType == TokenType.Tails)
            {
                var unblockedSticker = _stickers
                    .Where(s => s.Status == PositionStatus.InProgress && !s.Blocked)
                    .OrderByDescending(s => s.StepInProgress)
                    .FirstOrDefault();

                if (unblockedSticker != null)
                {
                    unblockedSticker.StepUp();
                    _tokens.RemoveAt(0);
                    return;
                }

                var blockedSticker = _stickers
                    .Where(s => s.Status == PositionStatus.InProgress && s.Blocked)
                    .OrderByDescending(s => s.StepInProgress)
                    .FirstOrDefault();

                if (blockedSticker != null)
                {
                    blockedSticker.Unblock();
                    _tokens.RemoveAt(0);
                    return;
                }




            }
            _tokens.RemoveAt(0);            
        }

        public void GiveTokenTo(Player player)
        {
            if (_tokens.Any(t => t.TokenType == TokenType.Eagle))
                throw new InvalidOperationException();

            SpendToken();
            player.AddToken(new Token(TokenType.Tails));
        }
    }
}