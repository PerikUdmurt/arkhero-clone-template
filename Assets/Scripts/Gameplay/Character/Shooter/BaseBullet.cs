using ArkheroClone.Infrastructure;
using System;
using System.Reflection.Emit;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public abstract class BaseBullet : MonoBehaviour, IPooledObject
    {

        public abstract void OnHit(GameObject gameObject);

        public virtual void OnCreated() { }

        public virtual void OnReceipt() { }

        public virtual void OnReleased() { }
    }
}