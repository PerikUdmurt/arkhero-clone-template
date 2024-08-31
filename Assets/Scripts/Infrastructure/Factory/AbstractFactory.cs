using Cysharp.Threading.Tasks;
using UnityEngine;
using ArkheroClone.Infrastructure.Bundles;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Infrastructure.Factory
{
    public class AbstractFactory<T> where T: MonoBehaviour 
    {
        private IBundleProvider _assetProvider;

        public AbstractFactory(IBundleProvider assetProvider)
            =>  _assetProvider = assetProvider;

        public async UniTask<T> CreateAsync(string bundlePath)
        {
            GameObject resource = await _assetProvider.LoadAsync<GameObject>(bundlePath);
            return CreateFromResource(resource);
        }

        public async UniTask<T> CreateAsync(AssetReference assetReference)
        {
            GameObject resource = await _assetProvider.LoadAsync<GameObject>(assetReference);
            return CreateFromResource(resource);
        }

        private static T CreateFromResource(GameObject resource)
        {
            GameObject obj = GameObject.Instantiate(resource, new Vector3(0, 0, 0), Quaternion.identity);
            obj.TryGetComponent<T>(out var result);
            return result;
        }
    }
}
    
