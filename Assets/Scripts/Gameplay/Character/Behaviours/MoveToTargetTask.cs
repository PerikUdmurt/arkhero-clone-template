using ArkheroClone.Infrastructure.BehaviourTree;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Behaviours
{
    public sealed class MoveToTargetTask : BehaviourNode
    {
        private readonly ITargetMover _targetMover;
        private readonly Vector3 _targetPosition;

        public MoveToTargetTask(ITargetMover mover)
        {
            _targetMover = mover;
        }

        public override NodeState Evaluate()
        {
             GetData("Target");

                _targetMover.MoveTo(_targetPosition);

            return NodeState.Running;
        }
    }

    public sealed class FindTargetTask : BehaviourNode
    {
        private readonly Collider _collider;
        private readonly float _range;
        private readonly LayerMask _targetMask;
        private TargetFinder _targetFinder;

        public FindTargetTask(Collider collider ,float range, LayerMask targetMask)
        {
            _targetFinder = new TargetFinder();
            _collider = collider;
            _range = range;
            _targetMask = targetMask;
        }

        public override NodeState Evaluate()
        {
            if (!_targetFinder.FindNearestTarget(_collider, _range, _targetMask, out var target))
                return NodeState.Failure;
            
            return NodeState.Success;
        }
    }
}