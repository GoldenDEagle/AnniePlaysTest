using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Projectiles
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;

        private ObjectPool<Projectile> _pool;

        public static ProjectilePool Instance;
        public ObjectPool<Projectile> Pool => _pool;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _pool = new ObjectPool<Projectile>(SpawnTarget, OnPoolGet, OnPoolRelease, OnPoolDestroy);
        }

        private Projectile SpawnTarget()
        {
            Projectile projectile = Instantiate(_projectilePrefab);
            projectile.SetPool(_pool);
            return projectile;
        }

        // pooling
        private void OnPoolGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void OnPoolRelease(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnPoolDestroy(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}