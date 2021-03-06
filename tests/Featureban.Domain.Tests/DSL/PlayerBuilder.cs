﻿using System.Collections.Generic;
using Featureban.Domain.Interfaces;
using Moq;

namespace Featureban.Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private IStickersBoard _stickersBoard;
        private ICoin _coin;
        private TokensPull _tokensPull;
        private readonly List<Token> _tokens;
        private string _name;

        public PlayerBuilder()
        {
            _tokens = new List<Token>();
            _stickersBoard = new StickersBoard(new Scale(2));
            _coin = new StubCoin(Token.Tails());
            _tokensPull = new TokensPull();
        }
        
        public PlayerBuilder WithTokens(int tokenCount)
        {
            for (var i = 0; i < tokenCount; i++)
            {
                _tokens.Add(Token.Eagle());
            }

            return this;
        }

        public PlayerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PlayerBuilder WithAlwaysEagleCoin()
        {
            var coinStub = new Mock<ICoin>();

            coinStub.Setup(c => c.MakeToss()).Returns(Token.Eagle());

            _coin = coinStub.Object;

            return this;
        }

        public PlayerBuilder WithBoard(IStickersBoard stickersBoard)
        {
            _stickersBoard = stickersBoard;
            return this;
        }       

        public Player Please()
        {
            var player = new Player(_name, _stickersBoard, _coin, _tokensPull);

            foreach (var token in _tokens)
            {
                player.AddToken(token);
            }

            return player;
        }

        public PlayerBuilder WithTailsToken()
        {
            _tokens.Add(Token.Tails());
            return this;
        }

        public PlayerBuilder With(TokensPull tokenPull)
        {
            _tokensPull = tokenPull;
            return this;
        }

        public PlayerBuilder WithEagleToken()
        {
            _tokens.Add(Token.Eagle());
            return this;
        }
    }
}