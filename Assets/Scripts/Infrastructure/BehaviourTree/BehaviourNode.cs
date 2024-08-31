using System.Collections.Generic;

namespace ArkheroClone.Infrastructure.BehaviourTree
{
    public class BehaviourNode : BaseNode
    {
        private NodeState _state;

        public BehaviourNode() : base() { }

        public BehaviourNode(List<BaseNode> nodes) : base(nodes) { }

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

            BaseNode node = _parent;
            
            while (node != null)
            {
                if (value != null)
                    return value;

                node = node.Parent;
            }

            return null;
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
            BehaviourNode node = (BehaviourNode)_parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;

                node = (BehaviourNode)node.Parent;
            }

            return false;
        }
    }
}