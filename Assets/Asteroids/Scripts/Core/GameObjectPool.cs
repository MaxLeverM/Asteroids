using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.Core
{
    public class GameObjectPool
    {
        private GameObject prefab;
        private Transform root;
        private ObjectPool<GameObject> objectPool;

        public ObjectPool<GameObject> ObjectPool => objectPool;

        public GameObjectPool(GameObject poolObject, Transform rootTransform, int defaultCapacity, int maxSize)
        {
            prefab = poolObject;
            root = rootTransform;
            objectPool = new ObjectPool<GameObject>(CreateObject, ActionOnGet, ActionOnRelease, ActionOnDestroy, true,
                defaultCapacity, maxSize);
        }

        private GameObject CreateObject()
        {
            return GameObject.Instantiate(prefab, root);
        }

        private void ActionOnGet(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void ActionOnRelease(GameObject obj)
        {
              obj.SetActive(false);
        }

        private void ActionOnDestroy(GameObject obj)
        {
            GameObject.Destroy(obj);
        }
    }
}