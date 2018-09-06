﻿using System.Collections.Generic;
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

        public IEnumerable<Sticker> GetStickersIn(ProgressPosition progressPosition)
        {
            return _progressSteps[progressPosition].AsReadOnly();
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
                sticker.ChangeStatus(StickerStatus.InProgress);
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
                if (_progressSteps[newPosition].Count == _wip)
                {
                    return;
                }

                _progressSteps[oldPosition].Remove(sticker);
                _progressSteps[newPosition].Add(sticker);

                sticker.ChangePositionNew(newPosition);
            }
            else
            {
                _progressSteps[oldPosition].Remove(sticker);
                sticker.ChangeStatus(StickerStatus.Done);
                DoneStickers++;
            }
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

        public bool CanCreateStickerInProgress()
        {
            return _wip == null || _progressSteps[ProgressPosition.First()].Count < _wip;
        }

        public Sticker GetMoveableStickerFor(Player player)
        {
            return _progressSteps
                .SelectMany(p => p.Value)
                .FirstOrDefault(s => s.Owner == player && !s.Blocked &&
                    (!_scale.IsValid(s.ProgressPosition.Next()) || 
                    _progressSteps[s.ProgressPosition.Next()].Count < _wip));
        }

        public void Setup(IEnumerable<Player> players)
        {
            foreach(var player in players)
            {
                var sticker = new Sticker(player);
                _progressSteps[ProgressPosition.First()].Add(sticker);
                sticker.ChangeStatus(StickerStatus.InProgress);
            }
        }

        public Player GetPlayerWichCanSpendToken()
        {
            throw new System.NotImplementedException();
        }
    }
}