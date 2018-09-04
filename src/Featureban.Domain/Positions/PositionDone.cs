using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Domain.Positions
{
    public sealed class PositionDone : Position
    {
        public PositionDone() : base(0)
        {
        }

        public override int GetHashCode()
        {
            return -1;
        }

        public override Position NextPosition()
        {
            return this;
        }
    }
}
