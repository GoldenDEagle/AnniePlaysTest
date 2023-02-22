using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private int _damage = 1;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector2 direction, float speed, int damage)
        {
            _damage = damage;
            _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
        }
    }
}