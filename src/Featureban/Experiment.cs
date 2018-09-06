using Featureban.Domain;

namespace Featureban
{

    class Experiment
    {
        private readonly ExperimentInputData _input;

        private float _result = 0;

        public Experiment( ExperimentInputData experimentInputData)
        {
            _input = experimentInputData;
        }

        public void DoExperiment(int iterationsCount)
        {
            int sumDoneStickers = 0;
            for (var i = 0; i < iterationsCount; i++)
            {
                var game = new Game(_input.PlayersCount, 2,
                                    _input.WipLimit, _input.RoundsCount);

                game.Setup();
                sumDoneStickers += game.GetDoneStickers();
            }

            _result = (sumDoneStickers * 1f) / iterationsCount;
        }

        public ExperimentOutput GetResult()
        {
            return new ExperimentOutput(
                _input.PlayersCount,
                _input.WipLimit,
                _input.RoundsCount,
                _result);
        }
    }
}
