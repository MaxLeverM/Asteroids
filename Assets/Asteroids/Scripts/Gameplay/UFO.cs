using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Starship;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class UFO : MonoBehaviour, IMovableObjectHolder, IDestroyable, IEnemyAI
    {
        [SerializeField] private MovableSpaceObject spaceObject;
        [SerializeField] private SpaceEngine spaceEngine;

        private UfoEnemyAI ufoEnemyAI;

        public Action<GameObject> DestroyCalled { get; set; }
        public MovableSpaceObject MovableObject => spaceObject;


        private void Start()
        {
            spaceObject.BindTransformPosition(transform);
            spaceEngine.Init(spaceObject, transform);
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