using ArkheroClone.StaticDatas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArkheroClone.Services.StaticDatas
{
    public class StaticDataService: IStaticDataService
    {
        private const string StaticDataPath = "StaticDatas";
        private Dictionary<string, StaticData> _staticDatas;

        public void LoadStaticDatas()
        {
            _staticDatas = Resources.LoadAll<StaticData>(StaticDataPath)
                .ToDictionary(x => x.StaticDataID, x => x);
        }

        public StaticData GetStaticData<T>(string staticDataName, out T itemStaticData) where T : StaticData
        {
            StaticData data = _staticDatas.TryGetValue(staticDataName, out StaticData value) ? value : null;
            itemStaticData = data as T;
            return itemStaticData;
        }
    }
}

