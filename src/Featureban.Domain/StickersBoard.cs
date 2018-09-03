namespace Featureban.Domain
{
    public class StickersBoard
    {
        private Scale _scale;

        public StickersBoard(Scale scale)
        {
            _scale = scale;
        }

        public Sticker CreateSticker()
        {
            var workItem = new Sticker(_scale);
            return workItem;
        }
    }
}