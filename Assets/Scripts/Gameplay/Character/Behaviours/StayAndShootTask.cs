using ArkheroClone.Gameplay.Characters.Shooting;
using ArkheroClone.Infrastructure.BehaviourTree;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Behaviours
{
    public sealed class StayAndShootTask : BehaviourNode
    {
        private readonly IShooter _shooter;

        public StayAndShootTask(IShooter shooter)
        {
            _shooter = shooter;
        }

        public override NodeState Evaluate()
        {
            GameObject target = GetData("visibleTarget") as GameObject;

            _shooter.Shoot(target.transform.position);
            return NodeState.Running;
        }
    }
}