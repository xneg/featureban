﻿namespace Featureban.Domain
{
    public class Sticker
    {
        public Player Owner { get; }

        public bool Blocked { get; private set; }

        public ProgressPosition ProgressPosition { get; private set; }

        public Sticker(Player player)
        {
            Owner = player;
            ProgressPosition = ProgressPosition.First();
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unblock()
        {
            Blocked = false;
        }

        public void ChangePosition(ProgressPosition progressPosition)
        {
            ProgressPosition = progressPosition;
        }
    }
}