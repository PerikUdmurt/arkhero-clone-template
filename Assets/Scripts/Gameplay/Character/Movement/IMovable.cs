using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public interface IMovable
    {
        public void Move(Vector3 direction, float deltaTime);
        void SetSpeed(float newSpeed);
    }
}
