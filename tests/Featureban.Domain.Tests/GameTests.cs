using System.Linq;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameCreatesStickersEqualToPlayersCount_WhenSetup()
        {
            var game = new Game(5, 2, 3, 10);

            game.Setup();

            var createdStickers =
                (game.StickersBoard as StickersBoard).GetStickersIn(ProgressPosition.First())
                .ToList();

            Assert.Equal(5, createdStickers.Count);
        }
    }
}
