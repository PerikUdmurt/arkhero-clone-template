using System.Collections;

namespace ArkheroClone.Infrastructure.BehaviourTree
{
    public abstract class BaseBehaviourTree
    {
        protected private BehaviourNode _root;
        public virtual void Evaluate()
        {
            if (_root != null)
                _root.Evaluate();
        }
    }

    public class BehaviourTree : BaseBehaviourTree
    {
        public BehaviourTree(BehaviourNode node) 
        { 
            _root = node;
        }
    }
}