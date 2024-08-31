using UnityEngine;

namespace ArkheroClone.Datas
{
    public struct BulletData
    {
        public int damage;
        public float speed;
        public Vector3 direction;
        public LayerMask targetLayer;
        public bool piercing;
    }
}