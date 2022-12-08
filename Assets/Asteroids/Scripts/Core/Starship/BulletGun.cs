using System;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Core.Starship
{
    [Serializable]
    public class BulletGun : IGun, IScorer
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float velocity = 0.2f;
        [SerializeField] private int maxPoolCapacity = 40;
        [SerializeField] private float rechargeTime = 0.3f;

        private GameObjectPool gameObjectPool;
        private GameObject bulletContainer;
        private Transform holder;
        private float currentRechargeTime;
        private bool isFireActive;

        public event Action<int> OnPointsAwarded;

        private readonly Vector3 bulletContainerOffset = new Vector3(-10, 0, 0);

        public void Init(Transform holderTransform)
        {
            bulletContainer = new GameObject("BulletContainer");
            bulletContainer.transform.position = bulletContainerOffset;
            gameObjectPool = new GameObjectPool(bulletPrefab, bulletContainer.transform, maxPoolCapacity / 2,
                maxPoolCapacity);
            holder = holderTransform;
        }

        public void Fire(bool isActive)
        {
            isFireActive = isActive;
            if (isFireActive)
            {
                SpawnBullet();
                currentRechargeTime = 0f;
            }
        }

        private void SpawnBullet()
        {
            var bulletObject = gameObjectPool.ObjectPool.Get();
            var movableObjectHolder = bulletObject.GetComponent<IMovableObjectHolder>();
            var movableObject = movableObjectHolder.MovableObject;
            if (movableObjectHolder is IDestroyable destroyableObject)
            {
                destroyableObject.DestroyCalled += DestroyCalled;
            }

            if (movableObjectHolder is IScorer scorer)
            {
                scorer.OnPointsAwarded += OnPointsAwarded;
            }

            movableObject.Position = holder.position;
            movableObject.Velocity = holder.up * velocity;
        }

        public void Update()
        {
            if (!isFireActive)
                return;

            currentRechargeTime += Time.deltaTime;
            if (currentRechargeTime >= rechargeTime)
            {
                currentRechargeTime -= rechargeTime;
                SpawnBullet();
            }
        }

        public void DestroyCalled(GameObject gameObjectForDestroy)
        {
            gameObjectPool.ObjectPool.Release(gameObjectForDestroy);
        }
    }
}