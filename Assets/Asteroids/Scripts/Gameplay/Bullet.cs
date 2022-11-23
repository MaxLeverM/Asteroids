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
        
        public Action<GameObject> DestroyCalled { get; set; }
        public Action<int> OnPointsAwarded { get; set; }
        
        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransform(transform);
        }

        private void OnEnable()
        {
            StartCoroutine(SelfDestructionTimer());
        }

        private void FixedUpdate()
        {
            spaceObject.PhysicUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(destroyerTag))
            {
                CallDestroy();
                if (other.TryGetComponent(out IDestroyable destroyableObject))
                {
                    if (destroyableObject is IRewardPoints rewardPoints)
                    {
                        OnPointsAwarded?.Invoke(rewardPoints.Score);
                    }
                    destroyableObject.CallDestroy();
                }
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