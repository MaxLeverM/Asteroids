using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.ObjectPool
{
    public class GameViewPool
    {
        private BaseView prefab;
        private Transform root;
        private ObjectPool<BaseView> pool;

        public ObjectPool<BaseView> Pool => pool;

        public GameViewPool(BaseView poolObject, Transform rootTransform, int defaultCapacity, int maxSize)
        {
            prefab = poolObject;
            root = rootTransform;
            pool = new ObjectPool<BaseView>(CreateObject, ActionOnGet, ActionOnRelease, ActionOnDestroy, true,
                defaultCapacity, maxSize);
        }

        private BaseView CreateObject()
        {
            var obj = Object.Instantiate(prefab, root);
            obj.transform.SetParent(root);
            return obj;
        }

        private void ActionOnGet(BaseView obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void ActionOnRelease(BaseView obj)
        {
            obj.gameObject.SetActive(false);
            obj.ResetView();
        }

        private void ActionOnDestroy(BaseView obj)
        {
            Object.Destroy(obj);
        }
    }
}