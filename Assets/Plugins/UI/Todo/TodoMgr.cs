using GameBase.Tools;
using System.Collections.Generic;

namespace GameBase.UI
{
    public class TodoMgr : SingletonInstance<TodoMgr>
    {
        private List<TodoNode> _nodes = new();

        public void Add(TodoNode node)
        {
            _nodes.Add(node);
        }

        protected override void Update()
        {
            foreach (var node in _nodes)
            {
                node.View.Position = node.Target.Position + node.Offset;
                node.View.IsShow = node.IsDirty && node.Target.IsShow;
            }
        }
    }
}
