using ArkheroClone.Infrastructure.Bundles;
using ArkheroClone.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Gameplay.Platforms
{
    public class PlatformSpawner
    {
        private AbstractFactory<Platform> _factory;

        public PlatformSpawner(IBundleProvider bundleProvider)
        {
            _factory = new AbstractFactory<Platform>(bundleProvider);
        }

        public async UniTask CreateRandomPlatformsAsync(List<Transform> spawnPoints, List<AssetReference> platformRefs)
        {
            foreach (Transform point in spawnPoints)
            {
                AssetReference asset = GetRandomAssetRef(platformRefs);
                await CreatePlatformAsync(asset, point);
            }
        }

        private async UniTask<Platform> CreatePlatformAsync(AssetReference asset, Transform spawnPoint)
        {
            Platform platform = await _factory.CreateAsync(asset);
            platform.Init(spawnPoint);
            return platform;

        }


        private AssetReference GetRandomAssetRef(List<AssetReference> platformRefs)
        {
            int randomvalue = Random.Range(0, platformRefs.Count);
            return platformRefs[randomvalue];
        }
    }
}