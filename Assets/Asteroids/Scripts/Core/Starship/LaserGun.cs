using System;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Core.Starship
{
    [Serializable]
    public class LaserGun : IGun, IScorer
    {
        [SerializeField] private float shotDuration = 1f;
        [SerializeField] private float damagePerSec = 100f;
        [SerializeField] private float rechargeTime = 10f;
        [SerializeField] private int maxShotCount = 3;
        [SerializeField] private int shotCount = 3;
        [SerializeField] private float laserDistance = 20f;
        [SerializeField] private LayerMask layerMask;

        private float currentShotDuration;
        private float currentRechargeTime;

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

        public event Action<bool> OnShotStatusChanged;
        public event Action<int> OnShotCountChanged;
        public event Action<float> OnRechargeTimeChanged;
        public event Action<int> OnPointsAwarded;


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
            if (laserHit.transform != null)
            {
                if (laserHit.transform.TryGetComponent(out IHealth enemyObject))
                {
                    if (enemyObject.Health.HP - damagePerSec * Time.fixedDeltaTime <= 0 && enemyObject is IRewardPoints rewardPoints)
                        OnPointsAwarded?.Invoke(rewardPoints.Score);

                    enemyObject.Health.Damage(damagePerSec * Time.fixedDeltaTime);
                }
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