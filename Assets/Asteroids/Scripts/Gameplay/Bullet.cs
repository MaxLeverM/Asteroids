using System;
using System.Collections;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Bullet : MonoBehaviour, IMovableObjectHolder, IDestroyable, IScorer
    {
        [SerializeField] private MovableSpaceObject spaceObject;
        [SerializeField] private string destroyerTag;
        [SerializeField] private float timeToDestroy = 1f;
        [SerializeField] private float damage = 100f;

        public event Action<GameObject> DestroyCalled;
        public event Action<int> OnPointsAwarded;

        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransform(transform);
        }

        private void OnEnable()
        {
            StartCoroutine(SelfDestructionTimer());
        }

        private void OnDisable()
        {
            DestroyCalled = null;
            OnPointsAwarded = null;
        }

        private void FixedUpdate()
        {
            spaceObject.PhysicUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(destroyerTag))
            {
                if (other.TryGetComponent(out IHealth enemyObject))
                {
                    if (enemyObject.Health.HP - damage <= 0 && enemyObject is IRewardPoints rewardPoints)
                        OnPointsAwarded?.Invoke(rewardPoints.Score);

                    enemyObject.Health.Damage(damage);
                }

                CallDestroy();
            }
        }

        private IEnumerator SelfDestructionTimer()
        {
            yield return new WaitForSeconds(timeToDestroy);
            CallDestroy();
        }

        public void CallDestroy()
        {
            DestroyCalled?.Invoke(gameObject);
        }
    }
}