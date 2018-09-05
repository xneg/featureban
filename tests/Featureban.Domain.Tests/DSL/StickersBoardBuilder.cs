namespace Featureban.Domain.Tests.DSL
{
    public class StickersBoardBuilder
    {
        private Scale _scale;

        public StickersBoardBuilder()
        {
            _scale = new Scale(2);
        }
        public StickersBoard Please()
        {
            return new StickersBoard(_scale);
        }
    }
}