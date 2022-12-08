using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Asteroid : MonoBehaviour, IMovableObjectHolder, IDestroyable, IRewardPoints, IHealth
    {
        [SerializeField] private MovableSpaceObject spaceObject;
        [SerializeField] private Health health;
        public event Action<GameObject> DestroyCalled;
        public Health Health => health;

        public int Score { get; set; }
        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransform(transform);
            health.OnHealthEnded += CallDestroy;
        }
        
        private void OnDisable()
        {
            DestroyCalled = null;
        }

        private void FixedUpdate()
        {
            spaceObject.PhysicUpdate();
        }

        public void CallDestroy()
        {
            DestroyCalled?.Invoke(gameObject);
        }
    }
}