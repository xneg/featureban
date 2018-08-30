using System;
using System.Collections.Generic;
using System.Text;

namespace Featureban.Domain.Tests.DSL
{
   static class Create
    {
        public static WorkItemBuilder WorkItem()
        {
            return new WorkItemBuilder();
        }

        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }
    }
}
