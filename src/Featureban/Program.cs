using Featureban.Domain;
using System;

namespace Featureban
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputData = new ExperimentInputData(3, null, 15);
            var experiment = new Experiment(inputData);

            experiment.DoExperiment(1000);

            var experimentResult = experiment.GetResult();

            Console.WriteLine(ExperimentOutput.Caption());
            Console.WriteLine(experimentResult);

            Console.ReadLine();
        }
    }
}
