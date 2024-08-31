using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.StaticDatas;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public class Shooter<TBullet>: IShooter where TBullet : BaseBullet
    {
        private Gun<TBullet> _gun;

        public Shooter(IBundleProvider bundleProvider, GunStaticData gunData) 
        {
            _gun = new Gun<TBullet>(bundleProvider, gunData);
        }

        public void Shoot(Vector3 target)
            => _gun.Shoot(target);
    }
}