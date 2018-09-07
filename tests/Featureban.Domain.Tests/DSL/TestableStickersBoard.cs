using System;

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
            var max = 0;
            foreach (var position in _progressSteps.Keys)
            {
                if (_progressSteps[position].Count > max)
                {
                    max = _progressSteps[position].Count;
                }
            }

            if (max == 0)
            {
                max++;
            }

            return max;
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
            foreach (var position in _progressSteps.Keys)
            {
                for (var s = 0; s < max; s++)
                {
                    if (_progressSteps[position].Count <= s)
                    {
                        table[s] += $"               |";
                    }
                    else
                    {
                        table[s] += StickerToString(_progressSteps[position][s], position);
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

            foreach (var position in _progressSteps.Keys)
            {
                if (_wip == null)
                {
                    caption += " InProgress    |";
                }
                else
                {
                    caption += $" InProgress ({_wip})  |";
                }
            }

            caption += " Done |";

            return caption;
        }
    }
}
