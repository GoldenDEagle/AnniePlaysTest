using UnityEngine;

namespace Assets.Scripts.Projectiles.Pooling
{
    public abstract class BasePool<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected abstract T SpawnInstance();

        protected virtual void OnPoolGet(T poolObject)
        {
            poolObject.gameObject.SetActive(true);
        }

        protected virtual void OnPoolRelease(T poolObject)
        {
            poolObject.gameObject.SetActive(false);
        }

        protected virtual void OnPoolDestroy(T poolObject)
        {
            Destroy(poolObject.gameObject);
        }
    }
}