using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Asteroid : MonoBehaviour, IMovableObjectHolder, IDestroyable
    {
        [SerializeField] private MovableSpaceObject spaceObject;
        [SerializeField] private string destroyerTag;
        
        public Action<GameObject> DestroyCalled { get; set; }
        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransformPosition(transform);
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
    }
}