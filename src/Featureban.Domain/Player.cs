using System;
using System.Collections.Generic;

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
            _tokens.Add(new Token());
        }

        public void AddToken()
        {
            _tokens.Add(new Token());
        }

        public void SpendToken()
        {
            _tokens.RemoveAt(0);
        }
    }
}