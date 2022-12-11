using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Gun;
using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Starship;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay
{
    public class Starship : MonoBehaviour, IScorer, IDestroyable
    {
        [SerializeField] private MovableSpaceObject movableSpaceObject;
        [SerializeField] private SpaceEngine spaceEngine;
        [SerializeField] private BulletGun mainGun;
        [SerializeField] private LaserGun additionalGun;
        [SerializeField] private string enemyTag;

        private bool isMovePressed;
        private bool isDestroyed;

        public IGun MainGun => mainGun;
        public IGun AdditionalGun => additionalGun;
        public MovableSpaceObject MovableSpaceObject => movableSpaceObject;
        public event Action<int> OnPointsAwarded;
        public event Action<GameObject> DestroyCalled;

        private void Start()
        {
            Initialize();
            Subscribes();
        }

        private void Initialize()
        {
            movableSpaceObject.BindTransform(transform);
            spaceEngine.Init(movableSpaceObject, transform);
            mainGun.Init(transform);
            additionalGun.Init(transform);
        }

        private void Subscribes()
        {
            if (mainGun is IScorer scorer)
                scorer.OnPointsAwarded += OnPointsAwarded;
            if (additionalGun is IScorer additionalGunScorer)
                additionalGunScorer.OnPointsAwarded += OnPointsAwarded;
        }

        private void Update()
        {
            if(isDestroyed)
                return;
            
            if (isMovePressed)
                spaceEngine.Move(transform.up);

            spaceEngine.LookAtPoint();
            
            mainGun.Update();
        }

        private void FixedUpdate()
        {
            if(isDestroyed)
                return;
            
            movableSpaceObject.PhysicUpdate();
            additionalGun.FixedUpdate();
        }

        public void MovePressed(bool isPressed) => isMovePressed = isPressed;

        public void PointerPositionChanged(Vector3 lookAtPosition) => spaceEngine.UpdatePointPosition(lookAtPosition);

        public void Fire(bool isActive)
        {
            if(isDestroyed)
                return;
            mainGun.Fire(isActive);
        }

        public void AdditionalFire(bool isActive)
        {
            if(isDestroyed)
                return;
            additionalGun.Fire(isActive);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(enemyTag))
            {
                CallDestroy();
            }
        }

        public void CallDestroy()
        {
            isDestroyed = true;
            DestroyCalled?.Invoke(gameObject);
        }
    }
}