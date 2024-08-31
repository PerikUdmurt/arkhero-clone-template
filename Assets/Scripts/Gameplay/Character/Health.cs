using System;

namespace ArkheroClone.Gameplay.Characters
{
    public class Health
    {
        private int _maxHealth;
        private int _health;

        public Health(int health)
        {
            _health = health;
            _maxHealth = health;
        }

        public Health(int health, int maxHealth)
        {
            _health = health;
            _maxHealth = maxHealth;
        }

        public event Action<int, int> HealthChanged;
        public event Action OnLowHealth;

        public int CurrentHealth
        {
            get => _health;
            set
            {
                _health = value;
                if (_health > _maxHealth)
                {
                    _health = _maxHealth;
                }
                if (_health <= 0)
                {
                    _health = 0;
                    OnLowHealth?.Invoke();
                }
                HealthChanged?.Invoke(_health, _maxHealth);
            }
        }
        public int MaxHealth { get => _maxHealth; }

        public void GetDamage(int damage)
            => CurrentHealth -= damage;

        public void GetHealth(int healPoints)
            => CurrentHealth += healPoints;
    }
}