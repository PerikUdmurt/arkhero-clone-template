using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.ObjectPool;
using ArkheroClone.StaticDatas;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public class Gun<TBullet> where TBullet : BaseBullet
    {
        private ObjectPool<TBullet> _bulletPool;
        public Gun(IBundleProvider bundleProvider, GunStaticData gunStaticData)
        {
            _bulletPool = new ObjectPool<TBullet>(bundleProvider, gunStaticData.BulletReference);
        }

        public async void Shoot(Vector3 target)
        {
            TBullet bullet = await _bulletPool.Get();
            bullet.OnCollision += ReleaseBullet;
        }

        public void ReleaseBullet(BaseBullet bullet)
        {
            _bulletPool.Release((TBullet)bullet);
        }
    }
}