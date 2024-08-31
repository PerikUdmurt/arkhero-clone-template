using UnityEngine;

namespace ArkheroClone.Gameplay.Characters
{
    public sealed class MovementController: IMovable
    {
        private Transform _transform;
        private float _speed;

        public MovementController(Transform transform, float speed)
        {
            _speed = speed;
            _transform = transform;
        }

        public void SetSpeed(float newSpeed)
            => _speed = newSpeed;

        public void Move(Vector2 direction, float deltaTime)
        {
            Vector3 lookDirection = _transform.position + new Vector3(direction.x, 0, direction.y);
            _transform.LookAt(lookDirection);
            _transform.position += _transform.forward * (_speed * deltaTime);
        }
    }
}
