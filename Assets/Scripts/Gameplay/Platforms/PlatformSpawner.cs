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
            List<UniTask> tasks = new();

            foreach (Transform point in spawnPoints)
            {
                AssetReference asset = GetRandomAssetRef(platformRefs);
                tasks.Add(CreatePlatformAsync(asset, point));
            }

            await UniTask.WhenAll(tasks);
        }

        private async UniTask<Platform> CreatePlatformAsync(AssetReference asset, Transform spawnPoint)
        {
            Platform platform = await _factory.CreateAsync(asset);
            platform.Init(spawnPoint);
            await WaitAnimation(platform);
            return platform;

        }

        private UniTask WaitAnimation(Platform platform)
        {
            var tcs = new UniTaskCompletionSource();
            platform.AnimationCompleted += () => tcs.TrySetResult();
            return tcs.Task;
        }


        private AssetReference GetRandomAssetRef(List<AssetReference> platformRefs)
        {
            int randomvalue = Random.Range(0, platformRefs.Count);
            return platformRefs[randomvalue];
        }
    }
}