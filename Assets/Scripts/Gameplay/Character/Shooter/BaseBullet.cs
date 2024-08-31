using ArkheroClone.Datas;
using ArkheroClone.Infrastructure;
using System;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public abstract class BaseBullet : MonoBehaviour, IPooledObject, IMovable
    {
        public event Action<BaseBullet> OnCollision;
        
        protected private BulletData _data;

        public void Construct(BulletData bulletData)
        {
            _data = bulletData;
        }

        public abstract void OnHit(GameObject gameObject);

        public virtual void Move(Vector2 direction, float deltaTime)
        {
            Vector3 lookDirection = transform.position + new Vector3(direction.x, 0, direction.y);
            transform.LookAt(lookDirection);
            transform.position += transform.forward * (_data.speed * deltaTime);
        }

        public virtual void OnCreated() { }

        public void OnReceipt() { }

        public void OnReleased() { }

        public void SetSpeed(float newSpeed)
        {
            _data.speed = newSpeed;
        }

        private void Update()
        {
            Move(_data.direction, Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _data.targetLayer)
            {
                OnHit(collision.gameObject);
            }

            OnCollision?.Invoke(this);
        }
    }
}