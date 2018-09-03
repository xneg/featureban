﻿namespace Featureban.Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private Player _player;

        public PlayerBuilder()
        {
            _player = new Player();
        }
        
        public PlayerBuilder With(Sticker workItem)
        {
            _player.AddSticker(workItem);
            return this;
        }
        public PlayerBuilder WithTokens(int tokenCount)
        {
            for (var i = 0; i < tokenCount; i++)
            {
                _player.AddToken();
            }
            return this;
        }

        public Player Please()
        {            
            return _player;
        }
    }
}