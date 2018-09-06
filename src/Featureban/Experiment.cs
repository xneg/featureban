using Featureban.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban
{
    
    class Experiment
    {
        private readonly ExperimentInputData _input;
        public Experiment( ExperimentInputData experimentInputData)
        {
            _input = experimentInputData;
        }

        public float DoExperiment(int iterationsCount)
        {
            int sumDoneStickers = 0;
            for(var i =0; i< iterationsCount; i++)
            {
                var game = new Game(_input.PlayersCount, 2,
                                    _input.WipLimit, _input.RoundsCount);

                game.Setup();
                sumDoneStickers += game.GetDoneStickers();
            }

            return (sumDoneStickers * 1f) / iterationsCount;
        }
    }
}
