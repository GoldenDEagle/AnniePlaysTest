using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Projectiles.Pooling
{
    public class ParticlesPool : BasePool<HitParticles>
    {
        [SerializeField] private HitParticles _hitParticlesPrefab;

        private ObjectPool<HitParticles> _particlesPool;

        public ObjectPool<HitParticles> Pool => _particlesPool;

        public static ParticlesPool Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _particlesPool = new ObjectPool<HitParticles>(SpawnInstance, OnPoolGet, OnPoolRelease, OnPoolDestroy);
        }

        protected override HitParticles SpawnInstance()
        {
            HitParticles particles = Instantiate(_hitParticlesPrefab);
            particles.SetPool(_particlesPool);
            return particles;
        }
    }
}