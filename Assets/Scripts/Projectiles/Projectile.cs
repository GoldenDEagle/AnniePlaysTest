using UnityEngine;
using static UnityEngine.GraphicsBuffer;
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

        public void Launch(Vector3 direction, float speed, int damage)
        {
            _damage = damage;
            _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
        }

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