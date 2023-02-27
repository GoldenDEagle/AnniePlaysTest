using Assets.Scripts.Projectiles.Pooling;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class HitParticles : MonoBehaviour, IPoolable<HitParticles>
    {
        private ParticleSystem _particleSystem;
        private IObjectPool<HitParticles> _pool;

        public void SetPool(IObjectPool<HitParticles> pool) => _pool = pool;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            
        }

        private void OnEnable()
        {
            _particleSystem.Play();
        }

        public void OnPoolRelease()
        {
            _pool.Release(this);
        }

        public void OnParticleSystemStopped()
        {
            OnPoolRelease();
        }
    }
}