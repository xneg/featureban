using Featureban.Domain.Tests.DSL;
using System.Linq;
using Xunit;

namespace Featureban.Domain.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameCreatesStickersEqualToPlayersCount_WhenSetup()
        {
            var game = Create.Game().Please();           

            game.Setup();

            var createdStickers = (game.StickersBoard as StickersBoard)
                .GetStickersIn(ProgressPosition.First())
                .ToList();
            Assert.Equal(5, createdStickers.Count);
        }
    }
}
