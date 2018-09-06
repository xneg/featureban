using Featureban.Domain;
using System;

namespace Featureban
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputData = new ExperimentInputData(3, 2, 15);
            var experiment = new Experiment(inputData);

            var experimentResult = experiment.DoExperiment(1000);

            Console.WriteLine($"Done stickers: {experimentResult}");

            Console.ReadLine();
        }
    }
}
