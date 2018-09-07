namespace Featureban.Domain.Tests.DSL
{
    internal static class ExtensionMethods
    {
        public static int Token(this int tokenCount)
        {
            return tokenCount;
        }

        public static int Wip (this int wipCount)
        {
            return wipCount;
        }

        public static int Position(this int positionCount)
        {
            return positionCount;
        }
    }
}
