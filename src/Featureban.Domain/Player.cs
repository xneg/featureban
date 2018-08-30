using System;
using System.Collections.Generic;

namespace Featureban.Domain
{
    public class Player
    {
        private List<WorkItem> _workItems = new List<WorkItem>();

        public IEnumerable<WorkItem> WorkItems => _workItems;

        private List<Token> _tokens = new List<Token>();
        public IReadOnlyList<Token> Tokens => _tokens.AsReadOnly();

        public Player()
        {
        }

        public void AddWorkItem(WorkItem workItem)
        {
            _workItems.Add(workItem);
        }

        public void MakeToss()
        {
            _tokens.Add(new Token());
        }
    }
}