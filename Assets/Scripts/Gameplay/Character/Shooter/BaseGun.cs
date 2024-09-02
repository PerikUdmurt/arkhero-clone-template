using System;
using UnityEngine;

namespace ArkheroClone.Gameplay.Characters.Shooting
{
    public abstract class BaseGun
    {
        public abstract void Shoot(Vector3 target);
    }
}