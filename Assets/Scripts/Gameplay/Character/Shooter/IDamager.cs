using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public interface IDamager
    {
        void DoDamage(IDamagable damagable);
    }
}