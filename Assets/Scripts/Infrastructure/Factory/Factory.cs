using Cysharp.Threading.Tasks;
using UnityEngine;
using ArkheroClone.Infrastructure.Bundles;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Infrastructure.Factory
{
    public class Factory<T> where T : MonoBehaviour
    {
        private readonly string _bundlePath;
        private readonly AssetReference _assetRef;
        private AbstractFactory<T> _abstractFactory;
        private CreateMethod _createMethod;

        public Factory(IBundleProvider bundleProvider, string bundlePath)
        {
            _abstractFactory = new AbstractFactory<T>(bundleProvider);
            _bundlePath = bundlePath;
            _createMethod = CreateMethod.BundlePath;
        }

        public Factory(IBundleProvider bundleProvider, AssetReference assetRef)
        {
            _abstractFactory = new AbstractFactory<T>(bundleProvider);
            _assetRef = assetRef;
            _createMethod = CreateMethod.AssetReference;
        }

        private enum CreateMethod
        {
            AssetReference = 0,
            BundlePath = 1
        }

        public async UniTask<T> CreateAsync()
        {
            switch (_createMethod)
            {
                case CreateMethod.BundlePath:
                    return await _abstractFactory.CreateAsync(_bundlePath);
                case CreateMethod.AssetReference:
                    return await _abstractFactory.CreateAsync(_assetRef);
            }
                
            return default;
        }
    }

    
}
    
