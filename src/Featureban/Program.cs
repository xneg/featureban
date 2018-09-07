using System;

namespace Featureban
{
    class Program
    {
        //* Количество игроков(3, 5, 10)
        //* Количество раундов в каждой игре(15, 20)
        //* Количество игр(1000)
        //* Ограничение WIP(нет ограничения, 1, 2, 3, 4, 5)

        static void Main(string[] args)
        {
            //Console.WriteLine(ExperimentOutput.Caption());

            //for (var i = 0; i <= 5; i++)
            //{
            //    var inputData = i == 0 ?
            //        new ExperimentInputData(3, null, 15) :
            //        new ExperimentInputData(3, i, 15);

            //    var experimentResult = Experiment.DoExperiment(inputData, 1000);
            //    Console.WriteLine(experimentResult);
            //}

            //Console.WriteLine();

            //Console.WriteLine(ExperimentOutput.Caption());

            //for (var i = 0; i <= 5; i++)
            //{
            //    var inputData = i == 0 ?
            //        new ExperimentInputData(5, null, 15) :
            //        new ExperimentInputData(5, i, 15);

            //    var experimentResult = Experiment.DoExperiment(inputData, 1000);
            //    Console.WriteLine(experimentResult);
            //}

            //Console.WriteLine();

            Console.WriteLine(ExperimentOutput.Caption());

            for (var i = 0; i <= 60; i++)
            {
                var inputData = i == 0 ?
                    new ExperimentInputData(10, null, 20) :
                    new ExperimentInputData(10, i, 20);

                var experimentResult = Experiment.DoExperiment(inputData, 1000);
                Console.WriteLine(experimentResult);
            }

            Console.ReadLine();
        }
    }
}
