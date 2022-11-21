using System;
using System.Collections;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class Spawner
    {
        [SerializeField] private GameObject prefabForSpawn;
        [SerializeField] private int startCapacity = 20;
        [SerializeField] private int maxCapacity = 30;
        [SerializeField] private Vector2 spawnInterval;
        [SerializeField] private Vector2 velocityRange;

        private GameObjectPool gameObjectPool;
        private Vector2 fieldBound;

        private Action<IFieldObject> objectSpawned;
        private Action<IFieldObject> objectDestroyed;

        public void Init(MonoBehaviour root, Vector2 fieldBound, Action<IFieldObject> objectSpawned,
            Action<IFieldObject> objectDestroyed)
        {
            gameObjectPool = new GameObjectPool(prefabForSpawn, root.transform, startCapacity, maxCapacity);
            this.objectSpawned = objectSpawned;
            this.objectDestroyed = objectDestroyed;
            this.fieldBound = fieldBound;

            for (int i = 0; i < startCapacity; i++)
            {
                Spawn();
            }

            root.StartCoroutine(CycleSpawn());
        }

        public void Spawn() //TODO Remove getcomponent, maybe make objectpool<IMovableObject>
        {
            var movableObjectHolder = gameObjectPool.ObjectPool.Get().GetComponent<IMovableObjectHolder>();
            var movableObject = movableObjectHolder.MovableObject;

            if (movableObjectHolder is IDestroyable destroyableObject)
            {
                destroyableObject.DestroyCalled = DestroyCalled;
            }

            var pointOnEdge = ProjectMath.RandomPointOutsideRectangle(fieldBound.x * 2, fieldBound.y * 2);
            movableObject.Position = new Vector2(pointOnEdge.x - fieldBound.x, pointOnEdge.y - fieldBound.y);

            movableObject.Velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            movableObject.Velocity *= Random.Range(velocityRange.x, velocityRange.y);

            objectSpawned?.Invoke(movableObject);
        }

        public void DestroyCalled(GameObject gameObjectForDestroy)
        {
            gameObjectPool.ObjectPool.Release(gameObjectForDestroy);
            objectDestroyed?.Invoke(gameObjectForDestroy.GetComponent<IMovableObjectHolder>().MovableObject);
        }

        private IEnumerator CycleSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
                if (gameObjectPool.ObjectPool.CountActive < maxCapacity)
                    Spawn();
            }
        }
    }
}