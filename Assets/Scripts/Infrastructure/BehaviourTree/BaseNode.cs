using System.Collections.Generic;

namespace ArkheroClone.Infrastructure.BehaviourTree
{
    public abstract class BaseNode
    {
        protected private BaseNode _parent = null;
        protected private List<BaseNode> _children;
        protected private Dictionary<string, object> _datas;

        public BaseNode()
        {
            _children = new List<BaseNode>();
            _parent = null;
        }

        public BaseNode(List<BaseNode> children)
        {
            _children = new List<BaseNode>();
            foreach (BaseNode child in children)
                AddNode(child);
        }

        public BaseNode Parent { get => _parent; }
        public List<BaseNode> Children { get => _children; }

        public void AddNode(BaseNode node)
        {
            node.SetParent(this);
            _children.Add(node);
        }

        public void RemoveNode(BaseNode node)
        {
            if (_children.Contains(node))
            {
                _children.Remove(node);
                node.SetParent(null);
            }
        }

        public void SetParent(BaseNode parent)
            => _parent = parent;
    }
}