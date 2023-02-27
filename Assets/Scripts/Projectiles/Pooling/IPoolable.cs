using UnityEngine.Pool;

namespace Assets.Scripts.Projectiles.Pooling
{
    public interface IPoolable<T> where T : class
    {
        public void SetPool(IObjectPool<T> pool);
        public void OnPoolRelease(); 
    }
}
