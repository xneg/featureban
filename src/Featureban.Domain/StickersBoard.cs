using System;
using System.Collections.Generic;
using System.Linq;
using Featureban.Domain.Interfaces;

namespace Featureban.Domain
{
    public class StickersBoard : IStickersBoard
    {
        private readonly Scale _scale;
        private readonly int? _wip;

        private readonly Dictionary<ProgressPosition, List<Sticker>> _progressSteps;

        public int DoneStickers { get; private set; }

        public StickersBoard(Scale scale, int? wip = null)
        {
            _scale = scale;
            _progressSteps = new Dictionary<ProgressPosition, List<Sticker>>();
            _wip = wip;

            CreateNewPartitions(_scale);
        }        

        public Sticker GetBlockedStickerFor(Player player)
        {
           return 
                _progressSteps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && s.Blocked);
        }

        public Sticker CreateStickerInProgress(Player player)
        {
            var sticker = new Sticker(player);

            if (CanCreateStickerInProgress())
            {
                _progressSteps[ProgressPosition.First()].Add(sticker);
            }

            return sticker;
        }

        public Sticker GetUnblockedStickerFor(Player player)
        {
            return
                _progressSteps
                .SelectMany(p => p.Value)
                // todo: сделать сортировку по позиции
                .FirstOrDefault(s => s.Owner == player && !s.Blocked);
        }

        public void StepUp (Sticker sticker)
        {
            if (sticker.Blocked)
            {
                return;
            }

            var oldPosition = sticker.ProgressPosition;
            var newPosition = oldPosition.Next();

            if (_scale.IsValid(newPosition))
            {
                if (!CanMoveTo(newPosition))
                    return;

                _progressSteps[oldPosition].Remove(sticker);
                _progressSteps[newPosition].Add(sticker);

                sticker.ChangePosition(newPosition);
            }
            else
            {
                _progressSteps[oldPosition].Remove(sticker);
                DoneStickers++;
            }
        }

        public bool CanCreateStickerInProgress()
        {
            return CanMoveTo(ProgressPosition.First());
        }

        public Sticker GetMoveableStickerFor(Player player)
        {
            return _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => s.Owner == player && !s.Blocked &&
                    (!_scale.IsValid(s.ProgressPosition.Next()) || CanMoveTo(s.ProgressPosition.Next())));
        }

        public void Setup(IEnumerable<Player> players)
        {
            foreach(var player in players)
            {
                var sticker = new Sticker(player);
                _progressSteps[ProgressPosition.First()].Add(sticker);
            }
        }

        public Player GetPlayerThatCanSpendToken()
        {
            var movableSticker = 
                _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => !s.Blocked &&
                    (!_scale.IsValid(s.ProgressPosition.Next()) || CanMoveTo(s.ProgressPosition.Next())));

            if (movableSticker != null)
                return movableSticker.Owner;

            var blockedSticker =
                _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => s.Blocked);

            if (blockedSticker != null)
                return blockedSticker.Owner;

            return null;
        }

        public IEnumerable<Sticker> GetStickersIn(ProgressPosition progressPosition)
        {
            return _progressSteps[progressPosition].AsReadOnly();
        }

        private void CreateNewPartitions(Scale scale)
        {
            var position = ProgressPosition.First();

            while (scale.IsValid(position))
            {
                _progressSteps[position] = new List<Sticker>();
                position = position.Next();
            }
        }

        public bool CanMoveTo(ProgressPosition position)
        {
            return _wip == null || _progressSteps[position].Count < _wip;
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
            return  $" [{stickerOwnerName} {blocked}]         |";
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