using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Featureban.Domain.Tests
{
   public class ScaleTests
    {
        [Fact]
        public void CreateNewPosition()
        {
            var scale = new Scale();

            var position = scale.CreatePosition();

            Assert.Equal(Step.ToDo, position.Step);
        }
    }
}
