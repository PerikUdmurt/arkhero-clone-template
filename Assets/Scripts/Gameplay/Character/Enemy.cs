using ArkheroClone.Gameplay.Characters.Shooting;
using ArkheroClone.Infrastructure.BehaviourTree;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.StaticDatas;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ArkheroClone.Gameplay.Characters
{
    [RequireComponent(typeof(Collider))] 
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Character, IDamagable, ITargetMover
    {
        [SerializeField]
        private Transform _gunPoint;

        private Health _health;
        private IShooter _shooter;
        private Collider _collider;
        private NavMeshAgent _navAgent;

        public override event Action<Character> Died;

        public void Construct(EnemyStaticData staticData, IBundleProvider bundleProvider)
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _collider = GetComponent<Collider>();
            _health = new(staticData.Health);
            _shooter = new Shooter<Bullet>(bundleProvider, staticData.GunStaticData);
        }

        public override BehaviourNode SetupBehaviours()
        {
            return new Selector(new List<BehaviourNode>
            {
                //new MoveToTargetTask(this)
                /*
                new Sequence(new List<BaseNode>
                {
                    new CheckNearestVisibleTask(_collider, 10, LayerMask.NameToLayer("Enemy")),
                    new StayAndShootTask(_shooter)
                })
                */
            });
        }

        public void GetDamage(int damage)
            => ((IDamagable)_health).GetDamage(damage);

        public void MoveTo(Vector3 position)
            => _navAgent.SetDestination(position);

        public void OnDied()
            => Died?.Invoke(this);
    }
}