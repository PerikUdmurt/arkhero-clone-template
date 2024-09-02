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
        private GameObject _target;

        public CheckNearestVisibleTask(Collider source, float range , LayerMask targetLayerMask) 
        {
            _source = source;
            _range = range;
            _targetLayerMask = targetLayerMask;
        }

        public override NodeState Evaluate()
        {
            if (_target == null)
            {
                if (FindNearestTarget())
                    return NodeState.Success;
                else
                    return NodeState.Failure;
            }

            CheckDistanceToTarget();

            return NodeState.Failure;
        }

        public bool FindNearestTarget()
        {
            if (_targetFinder.FindNearestVisibleTarget(_source, _range, _targetLayerMask, out GameObject target))
            {
                _target = target;
                Parent.SetData("visibleTarget", _target);
                return true;
            }

            return false;
        }

        public NodeState CheckDistanceToTarget()
        {
            float distance = (_source.transform.position - _target.transform.position).magnitude;

            if (distance > _range)
            {
                Parent.ClearData("visibleTarget");
                _target = null;
                return NodeState.Failure;
            }
            return NodeState.Success;
        }
    }
}
