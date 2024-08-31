using ArkheroClone.Gameplay.Characters.Shooting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.StaticDatas
{
    [CreateAssetMenu(fileName = "NewHeroStaticData", menuName = "StaticDatas/Characters/Hero")]
    public class HeroStaticData : StaticData
    {
        public override string StaticDataID { get => HeroName; }
        
        [Header("HeroCharacteristic")]
        public string HeroName = "NewCharacter";
        public float Speed = 5.0f;
        public int Health = 100;

        public GunStaticData gunStaticData;

        [Header("AssetReference")]
        public AssetReference CharacterAsset;
    }
}