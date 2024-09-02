using System.Collections.Generic;

namespace ArkheroClone.Infrastructure.BehaviourTree
{
    public class Selector : BehaviourNode
    {
        public Selector() : base() { }
        public Selector(List<BehaviourNode> nodes) : base(nodes) { }

        public override NodeState Evaluate()
        {
            foreach (BehaviourNode node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        SetState(NodeState.Success);
                        return GetState();
                    case NodeState.Running:
                        SetState(NodeState.Running); 
                        return GetState();
                    default:
                        continue;
                }
            }

            SetState(NodeState.Failure);
            return GetState();
        }
    }
}