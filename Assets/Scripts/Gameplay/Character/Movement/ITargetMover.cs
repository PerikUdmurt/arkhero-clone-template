using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public interface ITargetMover
    {
        public void MoveTo(Vector3 position);
    }
}