using UnityEngine;
using UnityEngine.Pool;

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

            _pool = new ObjectPool<Projectile>(SpawnProjectile, OnPoolGet, OnPoolRelease, OnPoolDestroy);
        }

        private Projectile SpawnProjectile()
        {
            Projectile projectile = Instantiate(_projectilePrefab);
            projectile.SetPool(_pool);
            return projectile;
        }

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