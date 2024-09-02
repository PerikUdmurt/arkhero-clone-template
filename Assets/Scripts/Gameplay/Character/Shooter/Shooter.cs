using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.StaticDatas;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public class Shooter: IShooter
    {
        private BaseGun _gun;
        public Shooter(IBundleProvider bundleProvider, Transform gunPoint, GunStaticData gunData) 
        {
            _gun = new DefaultGun(bundleProvider, gunPoint, gunData);
        }

        public void Shoot(Vector3 target)
            => _gun.Shoot(target);
    }
}