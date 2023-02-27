using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Projectiles.Pooling
{
    public class ProjectilePool : BasePool<Projectile>
    {
        [SerializeField] private Projectile _projectilePrefab;

        private ObjectPool<Projectile> _projectilePool;

        public static ProjectilePool Instance;
        public ObjectPool<Projectile> Pool => _projectilePool;

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

            _projectilePool = new ObjectPool<Projectile>(SpawnInstance, OnPoolGet, OnPoolRelease, OnPoolDestroy);
        }

        protected override Projectile SpawnInstance()
        {
            Projectile projectile = Instantiate(_projectilePrefab);
            projectile.SetPool(_projectilePool);
            return projectile;
        }
    }
}