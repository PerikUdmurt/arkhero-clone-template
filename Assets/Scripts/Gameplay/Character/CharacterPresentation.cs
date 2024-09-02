using ArkheroClone.Infrastructure.BehaviourTree;
using System;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public abstract class Character : MonoBehaviour
    {
        private BehaviourTree _behaviourTree;

        public abstract event Action<Character> Died;

        public abstract BehaviourNode SetupBehaviours();

        private void Awake()
        {
            this.enabled = false;
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