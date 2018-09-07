using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Featureban.Domain.Tests.DSL
{
    internal class StickerBoardParser
    {
        private readonly StickersBoardBuilder _stickersBoardBuilder;
        private List<Player> _players;
        private string _state;

        public StickerBoardParser( string state)
        {
            _state = state;
            _stickersBoardBuilder = new StickersBoardBuilder();
            _players = new List<Player>();
        }

        public StickerBoardParser WithPlayer(Player player)
        {
            _players.Add(player);
            return this;
        }

        public StickerBoardParser Parse()
        {
            var lines = _state.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            ParseCaption(lines.First());

            for (var i = 1; i < lines.Length; i++)
            {
               ParseColumnState(lines[i]);
            }

            return this;
        }

        public StickersBoard Please()
        {
            Parse();
            return _stickersBoardBuilder.Please();
        }

        private void ParseColumnState(string line)
        {
            if(!line.Contains('['))
            {
                return;
            }

            var inProgressCount = GetScale(line);
            var position = ProgressPosition.First();
            var lastPosition = line.IndexOf("|", 0);

            for (var p = 0; p < inProgressCount; p++)
            {
                var nextPosition = line.IndexOf("|", lastPosition + 1);
                var column = line.Substring(lastPosition, nextPosition - lastPosition - 1);
                var pattern = "([A-Z])";
                var stickerParam = Regex.Matches(column, pattern)
                    .Select(w => w.Value);

                var player = _players.FirstOrDefault(pl => pl.Name == stickerParam.First());

                if(player == null)
                {
                    player = Create.Player().WithName(stickerParam.First()).Please();
                }


                if (stickerParam.Contains("B"))
                {
                    _stickersBoardBuilder.WithBlockedStickerInProgressFor(player, position);
                }
                else
                {
                    _stickersBoardBuilder.WithStickerInProgressFor(player, position);
                }

                position = position.Next();
                lastPosition = nextPosition;
            }
        }

        private void ParseCaption(string caption)
        {
            _stickersBoardBuilder.WithScale(GetScale(caption));

            var wip = GetWip(caption);
            if (wip != null)
            {
                _stickersBoardBuilder.WithWip((int)wip);
            }
        }

        private int? GetWip(string caption)
        {
            var pattern = "([0-9])";

            var wips = Regex.Matches(caption, pattern)
                .Select(w => w.Value)
                .Distinct();

            if (!wips.Any())
            {
                return null;
            }

            if (wips.Count() != 1)
            {
                throw new InvalidCastException("WIP не совпадают у всех колонок");
            }

            var wip = int.Parse(wips.First());

            return wip;
        }

        private static int GetScale(string line)
        {
            return line.Length - line.Replace("|", "").Length - 2;
        } 
    }
}
