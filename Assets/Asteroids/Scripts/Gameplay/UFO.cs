using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Starship;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class UFO : MonoBehaviour, IMovableObjectHolder, IDestroyable, IEnemyAI, IRewardPoints, IHealth
    {
        [SerializeField] private MovableSpaceObject spaceObject;
        [SerializeField] private SpaceEngine spaceEngine;
        [SerializeField] private int rewardForDestroy;
        [SerializeField] private Health health;
        private UfoEnemyAI ufoEnemyAI;

        public int Score { get=>rewardForDestroy; set=>rewardForDestroy = value; }
        public MovableSpaceObject MovableObject => spaceObject;
        public Health Health => health;
        public event Action<GameObject> DestroyCalled;

        private void Start()
        {
            spaceObject.BindTransform(transform);
            spaceEngine.Init(spaceObject, transform);
            health.OnHealthEnded += CallDestroy;
        }
        
        private void OnDisable()
        {
            DestroyCalled = null;
        }

        private void Update()
        {
            ufoEnemyAI.Update();
        }

        private void FixedUpdate()
        {
            spaceObject.PhysicUpdate();
        }

        public void CallDestroy()
        {
            DestroyCalled?.Invoke(gameObject);
        }

        public void SetTarget(Transform target)
        {
            ufoEnemyAI ??= new UfoEnemyAI(transform, spaceEngine);
            ufoEnemyAI.SetTarget(target);
        }
    }
}