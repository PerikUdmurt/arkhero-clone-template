using ArkheroClone.StaticDatas;

namespace ArkheroClone.Services
{
    public interface IStaticDataService
    {
        StaticData GetStaticData<T>(string staticDataName, out T itemStaticData) where T : StaticData;
        void LoadStaticDatas();
    }
}