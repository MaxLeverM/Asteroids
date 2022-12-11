using System;
using System.Collections;
using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Core
{
    [Serializable]
    public class UfoSpawner
    {
        [SerializeField] private GameObject prefabForSpawn;
        [SerializeField] private int startCapacity = 20;
        [SerializeField] private int maxCapacity = 30;
        [SerializeField] private Vector2 spawnInterval;

        private GameObjectPool gameObjectPool;
        private Vector2 fieldBound;
        private Transform playerShip;

        private event Action<IFieldObject> objectSpawned;
        private event Action<IFieldObject> objectDestroyed;

        public void Init(MonoBehaviour root, Transform playerShip , Vector2 fieldBound, Action<IFieldObject> objectSpawned, Action<IFieldObject> objectDestroyed)
        {
            gameObjectPool = new GameObjectPool(prefabForSpawn, root.transform, startCapacity, maxCapacity);
            this.objectSpawned = objectSpawned;
            this.objectDestroyed = objectDestroyed;
            this.fieldBound = fieldBound;
            this.playerShip = playerShip;

            root.StartCoroutine(CycleSpawn());
        }

        private void Spawn()
        {
            var pointOnEdge = ProjectMath.RandomPointOutsideRectangle(fieldBound.x * 2, fieldBound.y * 2);
            var spawnPosition = new Vector2(pointOnEdge.x - fieldBound.x, pointOnEdge.y - fieldBound.y);

            Spawn(spawnPosition);
        }

        private void Spawn(Vector2 spawnPoint)
        {
            var spawnedObject = gameObjectPool.ObjectPool.Get();
            var movableObjectHolder = spawnedObject.GetComponent<IMovableObjectHolder>();
            var movableObject = movableObjectHolder.MovableObject;

            if (movableObjectHolder is IDestroyable destroyableObject)
            {
                destroyableObject.DestroyCalled += DestroyCalled;
            }

            if (movableObjectHolder is IEnemyAI enemyAI)
            {
                enemyAI.SetTarget(playerShip);
            }
            
            if (movableObjectHolder is IHealth healthObject)
            {
                healthObject.Health.HP = healthObject.Health.MaxHp;
            }

            movableObject.Position = spawnPoint;
            objectSpawned?.Invoke(movableObject);
        }

        private void DestroyCalled(GameObject gameObjectForDestroy)
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