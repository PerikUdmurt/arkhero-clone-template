using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ArkheroClone.Infrastructure.Factory;
using ArkheroClone.Infrastructure.Bundles;
using Cysharp.Threading.Tasks;
using ArkheroClone.Infrastructure;
using UnityEngine.AddressableAssets;

namespace ArkheroClone.Infrastructure.ObjectPool
{
    public class ObjectPool<T> where T: MonoBehaviour, IPooledObject
    {
        private Factory<T> _factory;
        private List<T> _objects = new List<T>();

        public ObjectPool(IBundleProvider assetProvider, string bundlePath)
        {
            _factory = new Factory<T>(assetProvider, bundlePath);
        }

        public ObjectPool(IBundleProvider assetProvider, AssetReference assetRef)
        {
            _factory = new Factory<T>(assetProvider, assetRef);
        }

        public async UniTask Fill(int prepareObjects)
        {
            for (int i = 0; i < prepareObjects; i++)
            {
                await Create();
            }
        }

        public void CleanUp()
        {
            foreach (var obj in _objects)
            {
                GameObject.Destroy(obj.gameObject);
            }
        }

        public async UniTask<T> Get()
        {
            var obj = _objects.FirstOrDefault(x => x.gameObject.activeSelf == false);

            if (obj == null)
            {
                obj = await Create();
            }
            obj.OnReceipt();
            return obj;
        }

        private async UniTask<T> Create()
        {
            T obj = await _factory.CreateAsync();
            _objects.Add(obj);
            obj.OnCreated();
            return obj;
        }

        public void Release(T obj)
        {
            obj.OnReleased();
        }
    }
}
