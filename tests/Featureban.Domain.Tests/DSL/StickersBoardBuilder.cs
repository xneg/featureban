namespace Featureban.Domain.Tests.DSL
{
    public class StickersBoardBuilder
    {
        private Scale _scale;

        public StickersBoardBuilder()
        {
            _scale = new Scale(2);
        }

        public StickersBoardBuilder WithScale(int positionsInProgress)
        {
            _scale = new Scale(positionsInProgress);
            return this;
        }
        public StickersBoard Please()
        {
            return new StickersBoard(_scale);
        }
    }
}