using Featureban.Domain.Interfaces;
using System;

namespace Featureban.Domain
{
    public class Player
    {
        private readonly IStickersBoard _stickersBoard;
        private readonly ICoin _coin;
        private readonly TokensPull _tokensPull;
        public string Name { get; }

        public Token Token { get; private set; }

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
            Token = token;
        }

        public void SpendToken()
        {
            if (Token == null)
            {
                return;
            }

            if (Token.IsEagle)
            {
                BlockSticker();

                TakeStickerToProgress();

                RemoveToken();
            }
            else if (Token.IsTails)
            {
                if (!(TryMoveSticker() || TryUnblockSticker() || TryTakeStickerToProgress()))
                {
                    GiveTokenToPull();
                }
                else
                {
                    RemoveToken();
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
            if (Token.IsEagle)
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
            Token = null;
        }
    }
}