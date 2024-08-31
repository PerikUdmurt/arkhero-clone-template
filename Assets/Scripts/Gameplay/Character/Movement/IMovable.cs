using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public interface IMovable
    {
        public void Move(Vector2 direction, float deltaTime);
        void SetSpeed(float newSpeed);
    }
}
