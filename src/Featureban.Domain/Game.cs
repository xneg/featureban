using Featureban.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    public class Game
    {
        private readonly List<Player> _players;

        private readonly TokensPull _tokensPull;

        private readonly int _roundsCount;

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
                var player = StickersBoard.GetPlayerThatCanSpendToken();
                if(player == null)
                {
                    if (StickersBoard.CanCreateStickerInProgress())
                        player = _players.First();
                    else
                        break;
                }
                player.TakeTokenFromPull();
                player.SpendToken();
            }

            _tokensPull.EraseTokens();
        }

        public int GetDoneStickers()
        {
            for (var i = 0; i < _roundsCount; i++)
                PlayRound();

            return StickersBoard.DoneStickers;
        }
    }
}