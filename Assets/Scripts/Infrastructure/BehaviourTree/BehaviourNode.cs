using System.Collections.Generic;
using UnityEngine;

namespace ArkheroClone.Infrastructure.BehaviourTree
{
    public class BehaviourNode
    {
        private NodeState _state;
        private Dictionary<string, object> _datas;
        private BehaviourNode _parent;
        private List<BehaviourNode> _children;

        public BehaviourNode()
        {
            _children = new List<BehaviourNode>();
            _parent = null;
            _datas = new Dictionary<string, object>();
        }

        public BehaviourNode(List<BehaviourNode> nodes)
        {
            _parent = null;
            _datas = new Dictionary<string, object>();
            _children = new List<BehaviourNode>();
            foreach (BehaviourNode child in nodes)
                AddNode(child);
        }

        public BehaviourNode Parent { get => _parent; }
        public List<BehaviourNode> Children { get => _children; }

        public virtual NodeState Evaluate() => NodeState.Failure;

        public NodeState GetState() => _state;

        public void SetState(NodeState state) => _state = state;

        public void SetData(string key, object value)
        {
            if (!_datas.ContainsKey(key))
                _datas.Add(key, value);

            _datas[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_datas.TryGetValue(key, out value))
                return value;

            BehaviourNode node = _parent;

            if (node != null)
            {
                value = node.GetData(key);
                if (value == null) return null;
            }

            return value;
        }

        public bool ClearData(string key)
        {
            if (_datas.ContainsKey(key))
            {
                _datas.Remove(key);
                return true;
            }

            return ContainsInParents(key);
        }

        private bool ContainsInParents(string key)
        {
            BehaviourNode node = _parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;

                node = node.Parent;
            }

            return false;
        }


        public void AddNode(BehaviourNode node)
        {
            node.SetParent(this);
            _children.Add(node);
        }

        public void RemoveNode(BehaviourNode node)
        {
            if (_children.Contains(node))
            {
                _children.Remove(node);
                node.SetParent(null);
            }
        }

        public void SetParent(BehaviourNode parent)
            => _parent = parent;
    }
}