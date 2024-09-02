using ArkheroClone.Gameplay.Characters.Behaviours;
using ArkheroClone.Gameplay.Characters.Shooting;
using ArkheroClone.Infrastructure.BehaviourTree;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.StaticDatas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    [RequireComponent(typeof(Collider))]
    public sealed class Player : Character, IDamagable
    {
        [SerializeField]
        private Transform _gunPoint;

        private Health _health;
        private IMovable _movementController;
        private IShooter _shooter;
        private InputService _inputService;
        private Collider _collider;

        private LayerMask _layerMask;

        public override event Action<Character> Died;

        public Health Health { get => _health; }

        public void Construct(HeroStaticData staticData, IBundleProvider bundleProvider, InputService inputService)
        {
            _layerMask = staticData.TargetLayers;

            _health = new(staticData.Health);
            _health.OnLowHealth += OnDied;
            _movementController = new MovementController(transform, staticData.Speed);
            _inputService = inputService;
            _shooter = new Shooter(bundleProvider, _gunPoint, staticData.GunStaticData);
            _collider = GetComponent<Collider>();
        }

        public override BehaviourNode SetupBehaviours()
        {
            return new Selector(new List<BehaviourNode>
            {
                new MoveTask(_movementController, _inputService),
                new Sequence(new List<BehaviourNode>
                {
                    new CheckNearestVisibleTask(_collider, 10, _layerMask),
                    new StayAndShootTask(transform, _shooter)
                })
            });
        }

        public void GetDamage(int damage)
            => _health.GetDamage(damage);

        public void OnDied()
            => Died?.Invoke(this);
        
    }
}