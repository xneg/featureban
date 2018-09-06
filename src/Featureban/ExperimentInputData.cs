namespace Featureban
{
    class ExperimentInputData
    {
        public int PlayersCount { get; }

        public int? WipLimit { get; }

        public int RoundsCount { get; }

        public ExperimentInputData(
            int playersCount,
            int? wipLimit,
            int roundsCount)
        {
            PlayersCount = playersCount;
            WipLimit = wipLimit;
            RoundsCount = roundsCount;
        }

    }
}
