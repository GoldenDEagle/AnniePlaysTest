using Assets.Scripts.UniversalComponents;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private IObjectPool<Projectile> _pool;

        private int _damage = 1;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // called every time projectile activates
        public void Launch(Vector3 direction, float speed, int damage)
        {
            _damage = damage;
            _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
        }

        public void ApplyDamage(GameObject target)
        {
            if (target.TryGetComponent(out HealthComponent health))
            {
                health.ModifyHealth(-_damage);
            }
        }

        // bind instance to pool
        public void SetPool(IObjectPool<Projectile> pool) => _pool = pool;

        public void OnPoolRelease()
        {
            _pool.Release(this);
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}