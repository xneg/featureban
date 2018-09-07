using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Featureban.Domain.Tests.DSL
{
    class StickerBoardParser
    {
        private StickersBoardBuilder _stickersBoardBuilder;
        public StickerBoardParser()
        {
            _stickersBoardBuilder = new StickersBoardBuilder();
        }
        public StickersBoardBuilder Parse(string state)
        {
            var lines = state.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            ParseCaption(lines.First());

            //todo: parse Stickers

            return _stickersBoardBuilder;

        }

        private void ParseColumnState(string c)
        {
            throw new NotImplementedException();
        }

        private void ParseCaption(string capton)
        {
            _stickersBoardBuilder.WithScale(GetScale(capton));

            var wip = GetWip(capton);
            if (wip != null)
            {
                _stickersBoardBuilder.WithWip((int)wip);
            }
        }

        private int? GetWip(string capton)
        {
            string pattern = "([0-9])";
            var wips = Regex.Matches(capton, pattern)
                .Select(w => w.Value)
                .Distinct();
            if (wips.Count() == 0)
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

        private static int GetScale(string capton)
        {
            return capton.Length - capton.Replace("|", "").Length - 2;
        }
    }
}
