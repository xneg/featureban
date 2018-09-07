using Featureban.Domain;

namespace Featureban
{

    static class Experiment
    {
        public static ExperimentOutput DoExperiment(ExperimentInputData input, int iterationsCount)
        {
            int sumDoneStickers = 0;
            for (var i = 0; i < iterationsCount; i++)
            {
                var game = new Game(input.PlayersCount, 2, input.WipLimit, input.RoundsCount);

                game.Setup();
                sumDoneStickers += game.GetDoneStickers();
            }

            var result = (sumDoneStickers * 1f) / iterationsCount;

            return new ExperimentOutput(
                input.PlayersCount,
                input.WipLimit,
                input.RoundsCount,
                result);
        }
    }
}
