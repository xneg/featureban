using System;

namespace Featureban
{
    public class ExperimentOutput
    {
        public int PlayersCount { get; }

        public int? WipLimit { get; }

        public int RoundsCount { get; }

        public float Result { get; }

        public ExperimentOutput(
            int playersCount,
            int? wipLimit,
            int roundsCount,
            float result)
        {
            PlayersCount = playersCount;
            WipLimit = wipLimit;
            RoundsCount = roundsCount;
            Result = result;
        }

        public override string ToString()
        {
            return $"|{PlayersCount, 15}|" +
                $"{WipLimit?.ToString() ?? "None", 11}|{RoundsCount, 14}|{Result, 8}|" 
                + Environment.NewLine +
                "-----------------------------------------------------";
        }

        public static string Caption()
        {
            return "| Players Count | WIP Limit | Rounds count | Result |" + Environment.NewLine +
                   "-----------------------------------------------------";
        }
    }
}
