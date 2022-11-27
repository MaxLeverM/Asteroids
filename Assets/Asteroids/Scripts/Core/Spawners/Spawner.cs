using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private List<AsteroidSpawnData> asteroidSpawnData;

        private GameObjectPool gameObjectPool;
        private Vector2 fieldBound;

        private Action<IFieldObject> objectSpawned;
        private Action<IFieldObject> objectDestroyed;

        public void Init(MonoBehaviour root, Vector2 fieldBound, Action<IFieldObject> objectSpawned, Action<IFieldObject> objectDestroyed)
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

        private void Spawn()
        {
            if (asteroidSpawnData.Count < 1)
                return;

            var pointOnEdge = ProjectMath.RandomPointOutsideRectangle(fieldBound.x * 2, fieldBound.y * 2);
            var spawnPosition = new Vector2(pointOnEdge.x - fieldBound.x, pointOnEdge.y - fieldBound.y);

            var velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            velocity *= Random.Range(asteroidSpawnData[0].VelocityRange.x, asteroidSpawnData[0].VelocityRange.y);

            Spawn(spawnPosition, velocity, asteroidSpawnData[0].Scale, asteroidSpawnData[0].ScoreForDestroy);
        }

        private void Spawn(Vector2 spawnPoint, Vector2 velocity, Vector3 scale, int score)
        {
            var spawnedObject = gameObjectPool.ObjectPool.Get().transform;
            var movableObjectHolder = spawnedObject.GetComponent<IMovableObjectHolder>();
            var movableObject = movableObjectHolder.MovableObject;

            if (movableObjectHolder is IDestroyable destroyableObject)
            {
                destroyableObject.DestroyCalled = DestroyCalled;
            }

            if (movableObjectHolder is IRewardPoints rewardPoints)
            {
                rewardPoints.Score = score;
            }

            spawnedObject.localScale = scale;
            movableObject.Position = spawnPoint;
            movableObject.Velocity = velocity;

            objectSpawned?.Invoke(movableObject);
        }

        private void DestroyCalled(GameObject gameObjectForDestroy)
        {
            gameObjectPool.ObjectPool.Release(gameObjectForDestroy);
            objectDestroyed?.Invoke(gameObjectForDestroy.GetComponent<IMovableObjectHolder>().MovableObject);

            DivideObject(gameObjectForDestroy.transform);
        }

        private void DivideObject(Transform originObject)
        {
            if (asteroidSpawnData.Count < 2)
                return;
            for (int i = 0; i < asteroidSpawnData.Count; i++)
            {
                if (originObject.localScale.magnitude >= asteroidSpawnData[i].Scale.magnitude)
                {
                    SpawnFragments(i + 1, originObject);
                    break;
                }
            }
        }

        private void SpawnFragments(int index, Transform originTransform)
        {
            for (int i = 0; i < asteroidSpawnData[index - 1].PartsCount; i++)
            {
                var velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                velocity *= Random.Range(asteroidSpawnData[index].VelocityRange.x,
                    asteroidSpawnData[index].VelocityRange.y);
                Spawn(originTransform.position, velocity, asteroidSpawnData[index].Scale, asteroidSpawnData[index].ScoreForDestroy);
            }
        }

        private IEnumerator CycleSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
                if (gameObjectPool.ObjectPool.CountActive < maxCapacity)
                    Spawn();
                if (gameObjectPool.ObjectPool.CountActive == 1)
                {
                    for (int i = 0; i < startCapacity - 1; i++)
                    {
                        Spawn();
                    }
                }
            }
        }
    }
}