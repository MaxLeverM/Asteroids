using System;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Core.Starship
{
    [Serializable]
    public class LaserGun : IGun
    {
        [SerializeField] private float shotDuration = 1f;
        [SerializeField] private float rechargeTime = 10f;
        [SerializeField] private int maxShotCount = 3;
        [SerializeField] private int shotCount = 3;
        [SerializeField] private float laserDistance = 20f;
        [SerializeField] private LayerMask layerMask;

        private float currentShotDuration;
       [SerializeField] private float currentRechargeTime;

        private bool isShotActive;
        private Transform holder;
        private RaycastHit2D laserHit;

        public bool IsShotActive
        {
            get => isShotActive;
            set
            {
                isShotActive = value;
                OnShotStatusChanged?.Invoke(isShotActive);
            }
        }
        public int ShotCount
        {
            get => shotCount;
            set
            {
                shotCount = value; 
                OnShotCountChanged?.Invoke(shotCount);
            }
        }
        public float CurrentRechargeTime
        {
            get => currentRechargeTime;
            set
            {
                currentRechargeTime = value;
                OnRechargeTimeChanged?.Invoke(currentRechargeTime);
            }
        }
        public float Distance => (laserHit.distance <= 0) ? laserDistance : laserHit.distance;
        public float RechargeTime => rechargeTime;
        public int MaxShotCount => maxShotCount;

        public Action<bool> OnShotStatusChanged;
        public Action<int> OnShotCountChanged;
        public Action<float> OnRechargeTimeChanged;

        public void Init(Transform holderTransform)
        {
            holder = holderTransform;
        }

        public void Fire(bool isActive)
        {
            if (isActive && !IsShotActive && shotCount > 0)
            {
                IsShotActive = true;
                ShotCount--;
            }
        }

        public void FixedUpdate()
        {
            if (IsShotActive)
            {
                LaserShot();
            }

            RefillShots();
        }

        private void LaserShot()
        {
            laserHit = Physics2D.Raycast(holder.position, holder.up, laserDistance, layerMask);
            if (laserHit.transform != null && laserHit.transform.TryGetComponent(out IDestroyable destroyableEnemy))
            {
                destroyableEnemy?.CallDestroy();
            }

            currentShotDuration += Time.fixedDeltaTime;
            if (currentShotDuration >= shotDuration)
            {
                IsShotActive = false;
                currentShotDuration = 0f;
            }
        }

        private void RefillShots()
        {
            if (shotCount < maxShotCount)
            {
                CurrentRechargeTime += Time.fixedDeltaTime;
                if (currentRechargeTime >= rechargeTime)
                {
                    ShotCount++;
                    CurrentRechargeTime = 0f;
                }
            }
        }
    }
}