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
            int[] playersCounts = new[] { 3, 5, 10 };
            int[] rounds = new[] { 15, 20 };
            int?[] wips = new[] {(int?)null, 1, 2, 3, 4, 5 };

            Console.WriteLine(ExperimentOutput.Caption());

            foreach (var playersCount in playersCounts)
            {
                foreach(var round in rounds)
                {
                    foreach(var wip in wips)
                    {
                        var inputData = new ExperimentInputData(playersCount, wip, round);
                        var experimentResult = Experiment.DoExperiment(inputData, 1000);

                        Console.WriteLine(experimentResult);
                    }

                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
    }
}
