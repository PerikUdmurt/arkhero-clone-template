using ArkheroClone.Datas;
using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.ObjectPool;
using ArkheroClone.StaticDatas;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public class DefaultGun : BaseGun
    {
        private readonly Transform _gunTransform;
        private readonly GunStaticData _gunStaticData;
        private ObjectPool<Bullet> _bulletPool;
        private BulletData _bulletData;
        private bool _cooldown;

        public DefaultGun(IBundleProvider bundleProvider, Transform gunTransform, GunStaticData gunStaticData)
        {
            _bulletPool = new ObjectPool<Bullet>(bundleProvider, gunStaticData.BulletReference);
            _gunTransform = gunTransform;
            _gunStaticData = gunStaticData;
            _bulletData = new BulletData()
            {
                damage = gunStaticData.Damage,
                speed = gunStaticData.BulletSpeed,
                piercing = false
            };
        }

        public override async void Shoot(Vector3 targetPosition)
        {
            if (_cooldown == true) return;

            Cooldown(_gunStaticData.ShootCoolDown);

            Bullet bullet = await _bulletPool.Get();
            bullet.Construct(_bulletData, _gunTransform.position, targetPosition);
            bullet.OnCollision += ReleaseBullet;
        }

        private async void Cooldown(float time)
        {
            _cooldown = true;
            await UniTask.WaitForSeconds(time);
            _cooldown = false;
        }

        private void ReleaseBullet(BaseBullet bullet)
        {
            Debug.Log("Release");
            _bulletPool.Release((Bullet)bullet);
        }
    
    }
}