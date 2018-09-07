namespace Featureban.Domain.Tests.DSL
{
    public class GameBuilder
    {
        public Game Please()
        {
            return new Game(5, 2, 3, 10);
        }
    }
}