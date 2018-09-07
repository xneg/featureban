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
        public string Name { get; }

        public Token Token => _tokens.FirstOrDefault();

        public Player(string name, IStickersBoard stickersBoard, ICoin coin, TokensPull tokensPull)
        {
            Name = name;
            _stickersBoard = stickersBoard;
            _coin = coin;
            _tokensPull = tokensPull;
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

                TakeStickerToProgress();
            }

            if (currentToken.IsTails)
            {
                if (!(TryMoveSticker() || TryUnblockSticker() || TryTakeStickerToProgress()))
                {
                    _tokensPull.IncrementToken();
                }
            }
        }

        public void TakeTokenFromPull()
        {
            if (_tokensPull.ContainsTokens)
            {
                _tokensPull.DecrementToken();
                AddToken(Token.Tails());
            }
        }

        public void GiveTokenToPull()
        {
            if (_tokens.Any(t => t.IsEagle))
                throw new InvalidOperationException();

            RemoveToken();
            _tokensPull.IncrementToken();
        }

        private void BlockSticker()
        {
            var sticker = _stickersBoard.GetUnblockedStickerFor(this);
                
            sticker?.Block();
        }

        private bool TryUnblockSticker()
        {
            var blockedSticker = _stickersBoard.GetBlockedStickerFor(this);

            if(blockedSticker != null)
            {
                blockedSticker.Unblock();
                return true;
            }

            return false;
        }        
        private bool TryMoveSticker()
        {
            var sticker = _stickersBoard.GetMoveableStickerFor(this);

            if (sticker != null)
            {
                _stickersBoard.StepUp(sticker);
                return true;
            }

            return false;
        }

        private bool TryTakeStickerToProgress()
        {
            if (_stickersBoard.CanCreateStickerInProgress())
            {
                _stickersBoard.CreateStickerInProgress(this);
                return true;
            }
            return false;
        }

        private void TakeStickerToProgress()
        {            
           _stickersBoard.CreateStickerInProgress(this);              
        }

        private void RemoveToken()
        {
            _tokens.Dequeue();
        }

        public override bool Equals(object obj)
        {
            var p = obj as Player;

            if ((object)p == null)
                return false;

            return p.Name == Name;
        }
        public bool Equals(Player p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return p.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Player p1, Player p2)
        {
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            if (((object)p1 == null) || ((object)p2 == null))
            {
                return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(Player p1, Player p2)
        {
            return !(p1 == p2);
        }
    }
}