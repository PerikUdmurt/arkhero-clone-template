using System.Collections.Generic;

namespace ArkheroClone.Infrastructure.BehaviourTree
{
    public sealed class Sequence : BehaviourNode
    {
        public Sequence() : base() { }
        public Sequence(List<BaseNode> nodes) : base(nodes) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (BehaviourNode node in Children)
            {
                switch (node.Evaluate()) 
                {
                    case NodeState.Failure:
                        SetState(NodeState.Failure);
                        return GetState();
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        SetState(NodeState.Success); 
                        return GetState();
                }
            }

            NodeState state = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            SetState(state);
            return state;
        }
    }
}