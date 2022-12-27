using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.ObjectPool
{
    public class GameObjectPool<T> where T: MonoBehaviour
    {
        private T prefab;
        private Transform root;
        private ObjectPool<T> pool;

        public ObjectPool<T> Pool => pool;

        public GameObjectPool(T poolObject, Transform rootTransform, int defaultCapacity, int maxSize)
        {
            prefab = poolObject;
            root = rootTransform;
            pool = new ObjectPool<T>(CreateObject, ActionOnGet, ActionOnRelease, ActionOnDestroy, true,
                defaultCapacity, maxSize);
        }

        private T CreateObject()
        {
            return Object.Instantiate(prefab, root);
        }

        private void ActionOnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(T obj)
        {
            Object.Destroy(obj);
        }
    }
}