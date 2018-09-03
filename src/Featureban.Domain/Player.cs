using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    public class Player
    {
        private List<Sticker> _stickers = new List<Sticker>();

        public IEnumerable<Sticker> Stickers => _stickers;

        private List<Token> _tokens = new List<Token>();
        public IReadOnlyList<Token> Tokens => _tokens.AsReadOnly();

        public Player()
        {
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
                var sticker = _stickers.FirstOrDefault(s => s.Status == PositionStatus.InProgress
                && !s.Blocked);

                sticker?.Block();
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