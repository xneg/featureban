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

        public void AddSticker(Sticker workItem)
        {
            _stickers.Add(workItem);
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
            _tokens.RemoveAt(0);
        }

        public void GiveTokenTo(Player player)
        {
            throw new InvalidOperationException();
        }
    }
}