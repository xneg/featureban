namespace Featureban.Domain.Interfaces
{
    public interface IStickersBoard
    {
        void CreateStickerInProgress(Player player);

        Sticker GetBlockedStickerFor(Player player);

        Sticker GetUnblockedStickerFor(Player player);

        void StepUp(Sticker sticker);
    }
}
