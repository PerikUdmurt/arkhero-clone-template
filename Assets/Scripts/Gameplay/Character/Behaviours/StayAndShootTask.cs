using ArkheroClone.Gameplay.Characters.Shooting;
using ArkheroClone.Infrastructure.BehaviourTree;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Behaviours
{
    public sealed class StayAndShootTask : BehaviourNode
    {
        private readonly Transform _transform;
        private readonly IShooter _shooter;

        public StayAndShootTask(Transform transform ,IShooter shooter)
        {
            _transform = transform;
            _shooter = shooter;
        }

        public override NodeState Evaluate()
        {
            GameObject target = GetData("visibleTarget") as GameObject;

            _transform.LookAt(target.transform);

            _shooter.Shoot(target.transform.position);
            return NodeState.Success;
        }
    }
}