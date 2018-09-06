namespace Featureban.Domain.Tests.DSL
{
    internal class StickersBoardBuilder
    {
        private Scale _scale;
        private int? _wip;

        public StickersBoardBuilder()
        {
            _scale = new Scale(2);
        }

        public StickersBoardBuilder WithScale(int positionsInProgress)
        {
            _scale = new Scale(positionsInProgress);
            return this;
        }

        public StickersBoardBuilder WithWip(int wipCount)
        {
            _wip = wipCount;
            return this;
        }
        public StickersBoard Please()
        {
            return new StickersBoard(_scale, _wip);
        }
    }
}