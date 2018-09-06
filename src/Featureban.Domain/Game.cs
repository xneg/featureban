using Featureban.Domain.Interfaces;
using System.Collections.Generic;

namespace Featureban.Domain
{
    public class Game
    {
        private List<Player> _players;
        private TokensPull _tokensPull;
        private int _roundsCount;

        public IStickersBoard StickersBoard { get; }

        public Game(
            int playersCount,
            int inProgressSteps,
            int? wipLimit,
            int roundsCount)
        {
            _roundsCount = roundsCount;
            _tokensPull = new TokensPull();
            StickersBoard = new StickersBoard(new Scale(inProgressSteps), wipLimit);           

            _players = new List<Player>();
            for (var i = 0; i < playersCount; i++)
            {
                _players.Add(new Player(StickersBoard, new Coin(), _tokensPull));
            }
        }

        public void Setup()
        {
            StickersBoard.Setup(_players);
        }

        public void PlayRound()
        {
            foreach(var player in _players)
            {
                player.MakeToss();
                player.SpendToken();
            }

            while (_tokensPull.ContainsTokens)
            {
                var player = StickersBoard.GetPlayerWichCanSpendToken();
                if(player == null)
                {
                    break;
                }
                player.TakeTokenFromPull();
                player.SpendToken();
            }

            _tokensPull.EraseTokens();
        }
        
    }
}