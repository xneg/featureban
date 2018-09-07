using System;
using System.Linq;

namespace Featureban.Domain.Tests.DSL
{
    internal class TestableStickersBoard : StickersBoard
    {
        public TestableStickersBoard(Scale scale, int? wip = null) : base(scale, wip)
        {
        }

        public override string ToString()
        {
            var max = GetMaxStickersInProgress();

            var caption = CaptionToString();

            var table = new string[max];

            for (var s = 0; s < max; s++)
            {
                table[s] += "|";
            }

            InProgressStickersToTable(max, ref table);
            InDoneStickersToTable(max, ref table);

            return caption + Environment.NewLine + string.Join(Environment.NewLine, table);
        }

        private int GetMaxStickersInProgress()
        {
            return Math.Max(1, _progressCells.Max(c => c.Stickers.Count));
        }

        private void InDoneStickersToTable(int max, ref string[] table)
        {
            table[0] += $" ({DoneStickers})   |";
            for (var s = 1; s < max; s++)
            {
                table[s] += "       |";
            }
        }

        private void InProgressStickersToTable(int max, ref string[] table)
        {
            foreach(var cell in _progressCells.OrderBy(c => c.Position.Step))
            {
                for (var s = 0; s < max; s++)
                {
                    if (cell.Stickers.Count <= s)
                    {
                        table[s] += $"               |";
                    }
                    else
                    {
                        table[s] += StickerToString(cell.Stickers[s], cell.Position);
                    }
                }
            }
        }

        private string StickerToString(Sticker sticker, ProgressPosition position)
        {
            var stickerOwnerName = sticker.Owner.Name;
            var blocked = sticker.Blocked ? "B" : " ";
            return $" [{stickerOwnerName} {blocked}]         |";
        }

        private string CaptionToString()
        {
            var caption = "|";

            foreach(var cell in _progressCells.OrderBy(c => c.Position.Step))
            {
                if (cell.Wip == null)
                {
                    caption += " InProgress    |";
                }
                else
                {
                    caption += $" InProgress ({cell.Wip})  |";
                }
            }

            caption += " Done |";

            return caption;
        }
    }
}
