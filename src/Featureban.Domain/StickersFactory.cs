namespace Featureban.Domain
{
    public class StickersFactory
    {
        private Scale _scale;

        public StickersFactory(Scale scale)
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