using System;
using System.Collections;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Bullet : MonoBehaviour, IMovableObjectHolder, IDestroyable
    {
        [SerializeField] private MovableSpaceObject spaceObject;
        [SerializeField] private string destroyerTag;
        [SerializeField] private float timeToDestroy = 1f;
        
        public Action<GameObject> DestroyCalled { get; set; }
        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransformPosition(transform);
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
                DestroyCalled?.Invoke(gameObject);
            }
        }

        private IEnumerator SelfDestructionTimer()
        {
            yield return new WaitForSeconds(timeToDestroy);
            DestroyCalled?.Invoke(gameObject);
        }
    }
}