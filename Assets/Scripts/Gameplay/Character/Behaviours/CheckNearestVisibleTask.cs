using ArkheroClone.Infrastructure.BehaviourTree;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Behaviours
{
    public class CheckNearestVisibleTask: BehaviourNode
    {
        private readonly Collider _source;
        private readonly float _range;
        private readonly LayerMask _targetLayerMask;
        private TargetFinder _targetFinder = new TargetFinder();
        public CheckNearestVisibleTask(Collider source, float range , LayerMask targetLayerMask) 
        {
            _source = source;
            _range = range;
            _targetLayerMask = targetLayerMask;
        }

        public override NodeState Evaluate()
        {
            if (FindNearestTarget())
                return NodeState.Success;
            else 
                return NodeState.Failure;
        }

        public bool FindNearestTarget()
        {
            if (_targetFinder.FindNearestVisibleTarget(_source, _range, _targetLayerMask, out GameObject target))
            {
                SetData("visibleTarget", target);
                return true;
            }

            return false;
        }
    }
}
