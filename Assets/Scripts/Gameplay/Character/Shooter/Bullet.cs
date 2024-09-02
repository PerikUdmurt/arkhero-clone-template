using ArkheroClone.Datas;
using System;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public class Bullet : BaseBullet, IDamager, IMovable
    {
        private BulletData _bulletData;
        private Vector3 _target;
        public event Action<Bullet> OnCollision;

        public void Construct(BulletData bulletData, Vector3 source, Vector3 targetPosition)
        {
            transform.position = source;
            _bulletData = bulletData;
            _target = targetPosition;
        }

        public override void OnHit(GameObject gameObject)
        {
            Debug.Log("hit");
            if (gameObject.TryGetComponent(out IDamagable damagable))
                DoDamage(damagable);
        }

        public void DoDamage(IDamagable damagableObject)
            => damagableObject.GetDamage(_bulletData.damage);

        public void Move(Vector3 direction, float deltaTime)
        {
            transform.LookAt(direction);
            transform.position += transform.forward * (_bulletData.speed * deltaTime);
        }

        public void SetSpeed(float newSpeed) { }

        private void Update()
            => Move(_target, Time.deltaTime);

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ddfdf");
            OnHit(other.gameObject);
            OnCollision?.Invoke(this);
        }
    }
}