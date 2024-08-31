using UnityEngine;

namespace ArkheroClone.StaticDatas
{
    public abstract class StaticData: ScriptableObject
    {
        public abstract string StaticDataID { get; }
    }
}