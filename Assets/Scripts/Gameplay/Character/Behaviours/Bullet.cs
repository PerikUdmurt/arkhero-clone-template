using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public class Bullet : BaseBullet, IDamager
    {
        public override void OnHit(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IDamagable damagable))
                DoDamage(damagable);
        }

        public void DoDamage(IDamagable damagableObject)
        {
            damagableObject.GetDamage(_data.damage);
        }
    }
}