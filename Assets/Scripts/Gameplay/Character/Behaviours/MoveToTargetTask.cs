using ArkheroClone.Infrastructure.BehaviourTree;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Behaviours
{
    public class MoveToTargetTask : BehaviourNode
    {
        private readonly ITargetMover _targetMover;
        private readonly Vector3 _targetPosition;

        public MoveToTargetTask(ITargetMover mover, Vector3 targetPosition)
        {
            _targetMover = mover;
            _targetPosition = targetPosition;
        }

        public override NodeState Evaluate()
        {
            _targetMover.MoveTo(_targetPosition);

            return NodeState.Running;
        }
    }
}