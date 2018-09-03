using System;

namespace Featureban.Domain.Tests.DSL
{
    public class StickerBuilder
    {        
        private Scale _scale;
        private bool _blocked;

        public StickerBuilder()
        {
            _scale = new Scale(2);
        }

        public Sticker Please()
        {
            var workItemsFactory = new StickersBoard(_scale);
            var workItem = workItemsFactory.CreateSticker();
            if(_blocked)
            {
                workItem.Block();
            }
            return workItem;
        }

        public StickerBuilder Blocked()
        {
            _blocked = true;
            return this;
        }
    }
}