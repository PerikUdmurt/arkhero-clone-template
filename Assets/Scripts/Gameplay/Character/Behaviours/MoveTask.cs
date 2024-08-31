using ArkheroClone.Infrastructure.BehaviourTree;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Behaviours
{
    public class MoveTask : BehaviourNode
    {
        private readonly IMovable _movable;
        private readonly InputService _inputService;

        public MoveTask(IMovable movable, InputService inputService)
        {
            _movable = movable;
            _inputService = inputService;
        }

        public override NodeState Evaluate()
        {
            if (_inputService.GetDirection().magnitude == 0)
                return NodeState.Failure;
                
            Debug.Log("Move");
            _movable.Move(_inputService.GetDirection(), Time.deltaTime);
            return NodeState.Running;
        }
    }
}