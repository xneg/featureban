using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Domain.Tests.DSL
{
   static class Create
    {
        public static StickerBuilder Sticker()
        {
            return new StickerBuilder();
        }

        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }
    }
}
