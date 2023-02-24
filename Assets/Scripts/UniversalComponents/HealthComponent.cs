using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UniversalComponents
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;

        [SerializeField] public UnityEvent _onDamage;
        [SerializeField] public UnityEvent _onDie;

        public int Health => _health;

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;

            _health += healthDelta;

            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }
}