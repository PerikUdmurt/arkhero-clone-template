using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.StaticDatas
{
    [CreateAssetMenu(fileName = "NewGunStaticData", menuName = "StaticDatas/Gun")]
    public class GunStaticData : StaticData
    {
        public override string StaticDataID { get => GunName; }
        
        public string GunName = "NewGun";

        public int Damage;
        public int NumOfProjectile;
        public float ShootCoolDown;
        public float BulletSpeed;
        public bool Piercing;

        public AssetReference BulletReference;
    }
}