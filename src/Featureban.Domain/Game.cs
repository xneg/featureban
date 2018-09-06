using Featureban.Domain.Interfaces;
using System.Collections.Generic;

namespace Featureban.Domain
{
    public class Game
    {
        private List<Player> _players;
        private IStickersBoard _stickersBoard;
        private TokensPull _tokensPull;
        private int _roundCount;


        public Game(int playersCount, int inProgressSteps,
            int? wipLimit, int roundsCount)
        {
            _roundCount = roundsCount;
            _tokensPull = new TokensPull();
            _stickersBoard = new StickersBoard(new Scale(inProgressSteps), wipLimit);           

            _players = new List<Player>();
            for (var i = 0; i < playersCount; i++)
            {
                _players.Add(new Player(_stickersBoard, new Coin(), _tokensPull ));
            }
        }
        
    }
}