using Featureban.Domain;
using System;

namespace Featureban
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(5, 2, 3, 15);

            game.Setup();
            var doneStickes = game.GetDoneStickers();

            Console.WriteLine($"Done stickers: {doneStickes}");

            Console.ReadLine();
        }
    }
}
