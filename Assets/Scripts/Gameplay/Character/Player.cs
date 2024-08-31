using ArkheroClone.Gameplay.Characters.Behaviours;
using ArkheroClone.Gameplay.Characters.Shooting;
using ArkheroClone.Infrastructure.BehaviourTree;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.StaticDatas;
using System.Collections.Generic;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    [RequireComponent(typeof(Collider))]
    public class Player : Character, IDamagable
    {
        private Health _health;
        private IMovable _movementController;
        private IShooter _shooter;
        private InputService _inputService;
        private Collider _collider;

        public void Construct(HeroStaticData staticData, IBundleProvider bundleProvider, InputService inputService)
        {
            _health = new(staticData.Health);
            _movementController = new MovementController(transform, staticData.Speed);
            _inputService = inputService;
            _shooter = new Shooter<Bullet>(bundleProvider, staticData.gunStaticData);
            _collider = GetComponent<Collider>();
        }

        public override BehaviourNode SetupBehaviours()
        {
            return new Selector(new List<BaseNode>
            {
                new MoveTask(_movementController, _inputService),
                new Sequence(new List<BaseNode>
                {
                    new CheckNearestVisibleTask(_collider, 10, LayerMask.NameToLayer("Enemy")),
                    new StayAndShootTask(_shooter)
                })
            });
        }

        public void GetDamage(int damage)
        {
            ((IDamagable)_health).GetDamage(damage);
        }
    }
}