﻿using System;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Core.Starship
{
    [Serializable]
    public class BulletGun : IGun
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

        public Action<IFieldObject> objectSpawned;
        public Action<IFieldObject> objectDestroyed;

        public void Init(Transform holderTransform)
        {
            bulletContainer = new GameObject("BulletContainer");
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
            var movableObjectHolder = gameObjectPool.ObjectPool.Get().GetComponent<IMovableObjectHolder>();
            var movableObject = movableObjectHolder.MovableObject;
            if (movableObjectHolder is IDestroyable destroyableObject)
            {
                destroyableObject.DestroyCalled = DestroyCalled;
            }

            movableObject.Position = holder.position;
            movableObject.Velocity = holder.up * velocity;
            objectSpawned?.Invoke(movableObject);
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
            objectDestroyed?.Invoke(gameObjectForDestroy.GetComponent<IMovableObjectHolder>().MovableObject);
        }
    }
}