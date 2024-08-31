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
            Collider target = (Collider)GetData("visibleTarget");
            _shooter.Shoot(target.bounds.center);
            return NodeState.Running;
        }
    }
}