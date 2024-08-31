using ArkheroClone.Infrastructure.BehaviourTree;
using System;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public abstract class Character : MonoBehaviour
    {
        private BehaviourTree _behaviourTree;

        public event Action<Character> OnDespawn;

        public abstract BehaviourNode SetupBehaviours();

        public virtual void Despawn()
        {
            OnDespawn?.Invoke(this);
        }

        private void Start()
        {
            _behaviourTree = new(SetupBehaviours());
        }

        private void Update()
        {
            _behaviourTree.Evaluate();
        }
    }
}