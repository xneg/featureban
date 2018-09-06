using System.Collections.Generic;

namespace Featureban.Domain.Interfaces
{
    public interface IStickersBoard
    {
        Sticker CreateStickerInProgress(Player player);

        Sticker GetBlockedStickerFor(Player player);

        Sticker GetUnblockedStickerFor(Player player);

        Sticker GetMoveableStickerFor(Player player);

        void Setup(IEnumerable<Player> players);

        void StepUp(Sticker sticker);

        bool CanCreateStickerInProgress();
    }
}
