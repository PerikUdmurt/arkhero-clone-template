using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Infrastructure.Bundles
{
    public interface IBundleProvider
    {
        void CleanUp();
        void Initialize();
        UniTask<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        UniTask<T> LoadAsync<T>(string address) where T : class;
    }
}