using Xunit;

namespace Featureban.Domain.Tests.DSL
{
   public static class AssertStickerBoard
    {
        public static void Equal(string expected, StickersBoard actual)
        {
            expected = expected.Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            var actualStr = actual.ToString().Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            Assert.Equal(expected, actualStr);
        }
    }
}
